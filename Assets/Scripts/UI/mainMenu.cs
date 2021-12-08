using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{

    public Text sensText;
    public Slider diffSlider;
    public Slider sensSlider;
    public Text difficulty;
    public GameObject settingsMenu;
    public Text dungScore;
    public Text dungTime;
    public Text survScore;
    public Text survTime;
    public GameObject highScoresMenu;
    public GameObject story;
    public GameObject startScreen;
    public GameObject extraScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetInt("Sensitivity", 4);
        }
    }
    private void Update()
    {
        if (settingsMenu.activeInHierarchy)
        {
            sensText.text = PlayerPrefs.GetInt("Sensitivity").ToString();
            sensSlider.value = PlayerPrefs.GetInt("Sensitivity");
            if (PlayerPrefs.GetInt("Difficulty") == 0)
            {
                difficulty.text = "Easy";
                diffSlider.value = PlayerPrefs.GetInt("Difficulty");
            }
            else if (PlayerPrefs.GetInt("Difficulty") == 1)
            {
                difficulty.text = "Medium";
                diffSlider.value = PlayerPrefs.GetInt("Difficulty");
            }
            else
            {
                difficulty.text = "Hard";
                diffSlider.value = PlayerPrefs.GetInt("Difficulty");
            }
        }
        if (highScoresMenu.activeInHierarchy)
        {
            dungScore.text = PlayerPrefs.GetInt("DungeonScore").ToString();
            dungTime.text = TimeScript.ToText((int)PlayerPrefs.GetFloat("DungeonTime"));
            survScore.text = PlayerPrefs.GetInt("SurvivalScore").ToString();
            survTime.text = TimeScript.ToText((int)PlayerPrefs.GetFloat("SurvivalTime"));
        }
    }
    public void gameQuit()
    {
        Application.Quit();
        print("Game exit");
    }

    public void gameContinue()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void dungeonStart()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void survialStart()
    {
        SceneManager.LoadScene("Survial");
    }
    public void TutorialStart()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void MouseSens(Slider slid)
    {
        PlayerStats.MouseSensitivity = slid.value;
        PlayerPrefs.SetInt("Sensitivity", (int)slid.value);
    }
    public void Difficulty(Slider slid1)
    {
        PlayerPrefs.SetInt("Difficulty", (int)slid1.value);
    }
    public void IsTutorialDone()
    {
        if (!PlayerPrefs.HasKey("HasDoneTutorial"))
        {
            story.SetActive(true);
            startScreen.SetActive(false);
        }
        else
        {
            extraScreen.SetActive(true);
            startScreen.SetActive(false);
        }
    }
}
