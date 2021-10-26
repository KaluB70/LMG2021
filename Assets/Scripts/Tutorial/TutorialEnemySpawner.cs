using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabNormal;
    public GameObject enemyPrefabFast;
    public GameObject enemyPrefabTank;
    public GameObject enemyPrefabFlying;
    public GameObject enemyPrefabShooting;
    public GameObject enemyPrefabSlowing;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public bool setActive = false;
    public GameObject UIHandler;
    public GameObject pickupGrapplinghook;

    void Update()
    {
        if (PlayerStats.Kills == 0 && setActive)
        {
            InstantiateEnemy(spawnPoint1, enemyPrefabNormal);
            setActive = false;
            TutorialUIScript.instance.setEnemyInfoNormal();
            UIHandler.GetComponent<TutorialUIScript>().setActive = true;
        }
        if (PlayerStats.Kills == 1 && !setActive)
        {
            InstantiateEnemy(spawnPoint2, enemyPrefabFast);
            setActive = true;
            TutorialUIScript.instance.setEnemyInfoFast();
            UIHandler.GetComponent<TutorialUIScript>().setActive = true;
        }
        if (PlayerStats.Kills == 2 && setActive)
        {
            InstantiateEnemy(spawnPoint3, enemyPrefabTank);
            setActive = false;
            TutorialUIScript.instance.setEnemyInfoTank();
            UIHandler.GetComponent<TutorialUIScript>().setActive = true;
        }
        if (PlayerStats.Kills == 3 && !setActive)
        {
            InstantiateEnemy(spawnPoint4, enemyPrefabFlying);
            setActive = true;
            TutorialUIScript.instance.setEnemyInfoFlying();
            UIHandler.GetComponent<TutorialUIScript>().setActive = true;
        }
        if (PlayerStats.Kills == 4)
        {
            if (pickupGrapplinghook != null)
            {
                pickupGrapplinghook.SetActive(true);
            }
        }
    }

    void InstantiateEnemy(Transform spawnpoint, GameObject enemyType)
    {
        Instantiate(enemyType, spawnpoint.position, spawnpoint.rotation);
    }
    
}
