using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject door;
    public EnemySpawner es;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            door.SetActive(true);
            if (es != null)
            {
                es.SetActive(PlayerStats.Kills);
            }
            
        }
    }
}
