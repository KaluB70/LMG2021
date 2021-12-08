using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public GameObject vScreen;
    public GameObject normalCanvas;
    public Text fTime;
    public Text fScore;
    int saveFinalS;
    int saveFinalT;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            saveFinalS = PlayerStats.Score;
            saveFinalT = TimeScript.ins.FinalTime();
            fScore.text = saveFinalS.ToString();
            fTime.text = TimeScript.ToText(saveFinalT);
            CheckHighScore();
            vScreen.SetActive(true);
            normalCanvas.SetActive(false);
        }
    }
    void CheckHighScore()
    {
        if (!PlayerPrefs.HasKey("DungeonScore"))
        {
            PlayerPrefs.SetInt("Dungeon Score", saveFinalS);
        }
        if (!PlayerPrefs.HasKey("DungeonTime"))
        {
            PlayerPrefs.SetFloat("DungeonTime", saveFinalT);
        }
        if (saveFinalS > PlayerPrefs.GetInt("DungeonScore"))
        {
            PlayerPrefs.SetInt("DungeonScore", saveFinalS);
        }
        if (saveFinalT < (int)PlayerPrefs.GetFloat("DungeonTime"))
        {
            PlayerPrefs.SetFloat("DungeonTime", saveFinalT);
        }
    }
}
