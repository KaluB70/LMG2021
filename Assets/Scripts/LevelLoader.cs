using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

    public GameObject choice1;
    public GameObject choice2;
    void Start()
    {
        choice1.SetActive(false);
        choice2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(KillCounterScript.instance.GetKills());
        if (KillCounterScript.instance.GetKills() == EnemySpawner.spawner.maxEnemies)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad); 
        }
    }
}
