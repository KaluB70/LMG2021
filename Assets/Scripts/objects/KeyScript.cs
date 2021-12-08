using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    public GameObject openedDoor;
    public EnemySpawner es;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenDoor();
            Destroy(this.gameObject, 0.1f);
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
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            Destroy(go, 0.1f);
        }
    }
}
