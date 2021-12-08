using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float characterSpeed = 17f;
    public float sprintSpeed = 5f;
    public float gravity = -30f;  //vaihdan editorissa pelaajan painovoimaksi -40 saadakseni hyppaamisesta nopean tuntuista
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask lavaMask;

    Vector3 velocity;
    bool isLava;
    bool isGrounded; //koskettaako pelaaja maata

    public bool canUseHookshot = false; //voiko hookshottia kayttaa
    public float cooldownTimer; //hookshotin cooldown
    public float cooldownAmount = 4f;
    public Text cooldownText;
    public Image cursor;
    public float hookshotDistance = 75f;
    public static bool slowActive = false;
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
        controller.slopeLimit = 90.0f;
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

       

        if (canUseHookshot)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownText.text = cooldownTimer.ToString("0.0");
            cooldownText.color = Color.red;


            if (cooldownTimer < 0.1)
            {
                cooldownTimer = 0;
                cooldownText.text = "";
                
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit) && raycastHit.distance < hookshotDistance)
                {
                    cursor.color = Color.green;
                }
                else
                {
                    cursor.color = Color.white;
                }

            }
        }
    }

    private void HandleMovement()
    {
        isLava = Physics.CheckSphere(groundCheck.position, 0.4f, lavaMask);
        //Kun pelaaja koskettaa maata, resetoidaan tippumisnopeus (painovoima)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isLava)
        {
            PlayerDeath.ins.Hitpoints -= Time.deltaTime*4;
            HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);
        }
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
        if (TestInputHookshot() && canUseHookshot)
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit) && raycastHit.distance < hookshotDistance && cooldownTimer == 0) //tarkistaa mihin pelaaja katsoo, etaisyyden ja cooldownin
            {
                cooldownTimer += cooldownAmount;
                cursor.color = Color.red;

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

    IEnumerator TempSlow()
    {
        float temp = characterSpeed;
        slowActive = true;
        characterSpeed *= 0.5f;
        yield return new WaitForSeconds(3);
        characterSpeed = temp;
        slowActive = false;
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