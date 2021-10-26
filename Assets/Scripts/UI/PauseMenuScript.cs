using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

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
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        PlayerStats.Score = 0;
    }
    public void Menu()
    {

    }
}
