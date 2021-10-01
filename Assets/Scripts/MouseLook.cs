using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 10f;

    public Transform playerBody;
    float xRotation = 0f;

    private Camera playerCamera;
    private float targetFov;
    private float fov;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        targetFov = playerCamera.fieldOfView;
        fov = targetFov;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   //pitää hiiren pelin sisällä kun peli käynnistetään (paina esc saadaksesi hiiren esille)
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //ei anna hiiren liikkua yli 90 astetta ylös tai alas

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);     //pyörittää pelaajahahmoa hiiren mukana

        //muutetaan field of view
        float fovSpeed = 4f;
        fov = Mathf.Lerp(fov, targetFov, Time.deltaTime * fovSpeed);
        playerCamera.fieldOfView = fov;
    }

    public void setCameraFov(float targetFov)
    {
        this.targetFov = targetFov;
    }
}
