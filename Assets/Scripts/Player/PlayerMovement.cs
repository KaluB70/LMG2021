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
    public Vector3 characterVelocityMomentum; //hookshotilla liikkumisessa kaytettava nopeus

    public CharacterController controller;

    public float characterSpeed = 8;
    public float sprintSpeed = 12;
    public float gravity = -30f;  //vaihdan editorissa pelaajan painovoimaksi -40 saadakseni hyppaamisesta nopean tuntuista
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded; //koskettaako pelaaja maata

    public bool canUse; //voiko hookshottia kayttaa

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
        if (Time.timeScale == 0)
        {
            return;
        }
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
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }

        if (TestInputJump() && isGrounded) //hyppy
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity += characterVelocityMomentum;
        velocity.y += gravity * Time.deltaTime;  //laskee pelaajan tippumisnopeuden
        controller.Move(velocity * Time.deltaTime);  //hahmo tippuu jatkuvasti painovoiman mukaisesti


        if (characterVelocityMomentum.magnitude >= 0f) //vaimentaa nopeuden esim. hookshotista hyppaamisen jalkeen
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

    //Jos pelaaja koskettaa maata tai kayttaa hookshottia, resetoidaan pelaajan painovoima
    public void ResetGravityEffect()
    {
        velocity.y = -2f;
    }

    public void HandleHookshotStart()
    {
        if (TestInputHookshot() && canUse)
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit)) //tarkistaa mihin pelaaja katsoo ja mista raycastilla
            {
                debugHitPointTransform.position = raycastHit.point; //siirtaa debug kuution raycastin osumaan kohtaan
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

        float hookshotThrowSpeed = 130f; //kuinka nopeasti hookshot heitetaan
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        //tarkisaa osuuko hookshot kohteeseen, lisaa FOV:ta ja siirtyy seuraavaan tilaan
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

        //hookshotin nopeus riippuu siita kuinka kaukaa se ammutaan
        //annetaan nopeimmat ja hitaimmat mahdolliset nopeudet
        float hookshotSpeedMin = 20f;
        float hookshotSpeedMax = 40f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 2f; //talla voi vaihtaa nopeutta helposti

        controller.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime); //liikuttaa pelaajan tahdattyyn kohtaan

        float reachedHookshotPositionDistance = 3f; //kohta jossa hookshot paastaa irti

        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance) //saavutaan kohteeseen normaalisti
        {
            StopHookshot();
        }

        if (TestInputHookshot()) //perutaan hookshotin kesken lentoa
        {
            StopHookshot();
        }

        if (TestInputJump()) //hypataan hookshottia kayttaessa
        {
            float momentumExtraSpeed = 3f; //lisavauhtia eteenpain
            characterVelocityMomentum = hookshotDir * hookshotSpeed * momentumExtraSpeed;

            float jumpSpeed = 30f; //lisavauhtia ylaspain
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