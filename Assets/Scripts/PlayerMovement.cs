using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform debugHitPointTransform; //debug kuutio jolla tarkistetaan mihin pelaaja katsoo esim hookshottia varten
    public Transform hookshotTransform;

    public Camera playerCamera;
    public MouseLook cameraFov;
    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 80f;

    public State state;

    public Vector3 hookshotPosition;
    public float hookshotSize;
    public Vector3 characterVelocityMomentum; //hookshotilla liikkumisessa k‰ytett‰v‰ nopeus

    public CharacterController controller;

    public float characterSpeed = 10;
    public float gravity = -9.81f;  //vaihdan editorissa pelaajan painovoimaksi -40 saadakseni hypp‰‰misest‰ nopean tuntuista
    public float jumpHeight = 5f; 

    public Transform groundCheck;  
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded; //koskettaako pelaaja maata

    public bool canUse; //voiko hookshottia k‰ytt‰‰

    public enum State
    {
        Normal,
        HookshotThrown,
        HookshotFlyingPlayer
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = transform.Find("Main Camera").GetComponent<Camera>();
        cameraFov = playerCamera.GetComponent<MouseLook>();

        state = State.Normal;

        hookshotTransform.gameObject.SetActive(false);

        //bool canUse = gameObject.GetComponent<PlayerStats>().canUse;
    }

    void Update()
    {
        switch (state) //pelaajan tila
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                HandleMovement();
                break;
            case State.HookshotThrown:
                HandleHookshotThrow();
                HandleMovement();
                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                break;
        }
    }
    private void HandleMovement()
    {
        //Kun pelaaja koskettaa maata, resetoidaan tippumisnopeus (painovoima)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * characterSpeed * Time.deltaTime); //kertoo charactercontrollerille miten liikutaan

        if (TestInputJump() && isGrounded) //hyppy
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity += characterVelocityMomentum;
        velocity.y += gravity * Time.deltaTime;  //laskee pelaajan tippumisnopeuden
        controller.Move(velocity * Time.deltaTime);  //hahmo tippuu jatkuvasti painovoiman mukaisesti
        

        if (characterVelocityMomentum.magnitude >= 0f) //vaimentaa nopeuden esim. hookshotista hypp‰‰misen j‰lkeen
        {
            float momentumDrag = 3f;
            velocity -= characterVelocityMomentum;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (characterVelocityMomentum.magnitude < .0f)
            {
                characterVelocityMomentum = Vector3.zero;
            }
        }
    }

    //Jos pelaaja koskettaa maata tai k‰ytt‰‰ hookshottia, resetoidaan pelaajan painovoima
    public void ResetGravityEffect()
    {
        velocity.y = -2f;
    }

    public void HandleHookshotStart()
    {
        if (TestInputHookshot() && canUse)
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit)) //tarkistaa mihin pelaaja katsoo ja mist‰ raycastilla
            {
                debugHitPointTransform.position = raycastHit.point; //siirt‰‰ debug kuution raycastin osumaan kohtaan
                hookshotPosition = raycastHit.point;
                hookshotSize = 0f;
                hookshotTransform.gameObject.SetActive(true);   
                hookshotTransform.localScale = Vector3.zero;
                state = State.HookshotThrown;
            }
        }
    }

    public void HandleHookshotThrow()
    {
        hookshotTransform.LookAt(hookshotPosition);

        float hookshotThrowSpeed = 130f; //kuinka nopeasti hookshot heitet‰‰n
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        //tarkisaa osuuko hookshot kohteeseen, lis‰‰ FOV:ta ja siirtyy seuraavaan tilaan
        if (hookshotSize >= Vector3.Distance(transform.position, hookshotPosition)) 
        {
            state = State.HookshotFlyingPlayer;
            cameraFov.setCameraFov(HOOKSHOT_FOV);
        }
    }

    public void HandleHookshotMovement()
    {
        hookshotTransform.LookAt(hookshotPosition);
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        //hookshotin nopeus riippuu siit‰ kuinka kaukaa se ammutaan
        //annetaan nopeimmat ja hitaimmat mahdolliset nopeudet
        float hookshotSpeedMin = 20f;
        float hookshotSpeedMax = 40f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 2f; //t‰ll‰ voi vaihtaa nopeutta helposti

        controller.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime); //liikuttaa pelaajan t‰hd‰ttyyn kohtaan

        float reachedHookshotPositionDistance = 3f; //kohta jossa hookshot p‰‰st‰‰ irti

        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance) //saavutaan kohteeseen normaalisti
        {
            StopHookshot();
        }

        if (TestInputHookshot()) //perutaan hookshotin kesken lentoa
        {
            StopHookshot();
        }

        if (TestInputJump()) //hyp‰t‰‰n hookshottia k‰ytt‰ess‰
        {
            float momentumExtraSpeed = 3f; //lis‰vauhtia eteenp‰in
            characterVelocityMomentum = hookshotDir * hookshotSpeed * momentumExtraSpeed;

            float jumpSpeed = 30f; //lis‰vauhtia ylˆsp‰in
            characterVelocityMomentum += Vector3.up * jumpSpeed;

            StopHookshot();
        }
    }

    public void StopHookshot() //resetoidaan liikkuvuus ja FOV, poistetaan hookshot objekti
    {
        state = State.Normal;
        ResetGravityEffect();
        hookshotTransform.gameObject.SetActive(false);
        cameraFov.setCameraFov(NORMAL_FOV);
    }

    public bool TestInputHookshot()
    {
        //return Input.GetKeyDown(KeyCode.E);
        return Input.GetButtonDown("Hookshot");
    }

    public bool TestInputJump()
    {
        return Input.GetButtonDown("Jump");
    }
}
