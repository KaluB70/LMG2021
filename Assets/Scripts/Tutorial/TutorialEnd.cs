using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EndTutorial();
        }
    }
    public void EndTutorial()
    {
        PlayerPrefs.SetString("HasDoneTutorial", "yes");
        SceneManager.LoadScene("Dungeon");
        PlayerStats.Kills = 0;
        PlayerStats.Score = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
