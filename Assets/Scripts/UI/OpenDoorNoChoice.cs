using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorNoChoice : MonoBehaviour
{
    public GameObject doorToOpen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            doorToOpen.SetActive(false);
        }
    }
}
