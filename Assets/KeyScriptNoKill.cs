using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScriptNoKill : MonoBehaviour
{

    //public GameObject pickupEffect;
    public GameObject openedDoor;
    public EnemySpawner es;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenDoor();
            Destroy(this, 0.1f);
        }
    }

    void OpenDoor()
    {
        print("Picked up a key");
        Choice.ins.ShowPopup(openedDoor);
        if (es != null)
        {
            es.SetInactive();
        }
    }
}
