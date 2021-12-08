using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceHandler : MonoBehaviour
{
    public GameObject doorToOpen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Choice.ins.ShowPopup(doorToOpen);
            Destroy(this);
        }
    }
}
