using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabNormal;
    public GameObject enemyPrefabFast;
    public GameObject enemyPrefabTank;
    public GameObject enemyPrefabFlying;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public bool setActive = false;
    public GameObject pickupGrapplinghook;
    public GameObject quideArrows;
    public static bool enemyTutDone;
    public static int state;
    private int kills;
    public GameObject player;

    private void Start()
    {
        enemyTutDone = false;
        state = 0;
    }

    void Update()
    {
        kills = PlayerStats.Kills;
        if (kills == 0 && setActive && state == 0)
        {
            setActive = false;
            InstantiateEnemy(spawnPoint1, enemyPrefabNormal);
            TutorialUIScript.instance.setEnemyInfoNormal();
            TutorialUIScript.instance.Open();
            state++;
        }
        if (kills == 1 && state == 1)
        {
            InstantiateEnemy(spawnPoint2, enemyPrefabFast);
            TutorialUIScript.instance.setEnemyInfoFast();
            TutorialUIScript.instance.Open();
            state++;
        }
        if (kills == 2 && state == 2)
        {
            InstantiateEnemy(spawnPoint3, enemyPrefabTank);
            TutorialUIScript.instance.setEnemyInfoTank();
            TutorialUIScript.instance.Open();
            state++;
        }
        if (kills == 3 && state == 3)
        {
            InstantiateEnemy(spawnPoint4, enemyPrefabFlying);
            TutorialUIScript.instance.setEnemyInfoFlying();
            TutorialUIScript.instance.Open();
            state++;
        }
        if (kills == 4)
        {
            if (pickupGrapplinghook != null)
            {
                pickupGrapplinghook.SetActive(true);
                quideArrows.SetActive(true);
                enemyTutDone = true;
            }
        }

        void InstantiateEnemy(Transform spawnpoint, GameObject enemyType)
        {
            Instantiate(enemyType, spawnpoint.position, spawnpoint.rotation);
        }
    }
}
