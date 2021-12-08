using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MouseLook : MonoBehaviour
    {

        public float mouseSensitivity;// = 10f;

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
            mouseSensitivity = PlayerPrefs.GetInt("Sensitivity");
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;   //pitaa hiiren pelin sisalla kun peli kaynnistetaan (paina esc saadaksesi hiiren esille)
        }


        void Update()
        {
        if (Time.timeScale == 0)
        return;
            mouseSensitivity = PlayerPrefs.GetInt("Sensitivity") > 0 ? PlayerPrefs.GetInt("Sensitivity") : 1;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //ei anna hiiren liikkua yli 90 astetta ylas tai alas

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);     //pyarittaa pelaajahahmoa hiiren mukana

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