using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats ins;
    public static int Kills;
    public static int Score;
    public static float MouseSensitivity;
    void Start()
    {
        ins = this;
        Kills = 0;
        Score = 0;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode lsm)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(lsm);
        Kills = 0;
        Score = 0;
    }
}

