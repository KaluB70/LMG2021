using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public Text sensText;
    public Slider sensSlider;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool open = !pauseMenu.activeInHierarchy;
            pauseMenu.SetActive(open);
            Time.timeScale = open ? 0 : 1;
            Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (settingsMenu.activeInHierarchy)
        {
            sensText.text = PlayerPrefs.GetInt("Sensitivity").ToString();
            sensSlider.value = PlayerPrefs.GetInt("Sensitivity");
        }
    }

    public void Continue()
    {
        GameObject.FindGameObjectWithTag("PauseMenu").SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        PlayerStats.Score = 0;
        PlayerStats.Kills = 0;
        Burstfire.duration = 10f;
        Pickup_CalUp.duration = 10f;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Mainmenu");
        Time.timeScale = 1;
        PlayerStats.Score = 0;
        PlayerStats.Kills = 0;
    }
    public void MouseSens(Slider slid)
    {
        PlayerStats.MouseSensitivity = slid.value;
        PlayerPrefs.SetInt("Sensitivity", (int)slid.value);
    }
}
