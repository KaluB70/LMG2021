using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public Transform spawnPoint;
    public TutorialEnemySpawner tes;
    public GameObject player;
    public GameObject trigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (TutorialEnemySpawner.enemyTutDone)
            {
                other.transform.position = spawnPoint.position;
            }
            else 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //PlayerStats.Kills = 0;
                //PlayerStats.Score = 0;
            }
        }
        
    }
}
