using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : DeathScript
{

    public static PlayerDeath ins;
    public GameObject dScreen;
    public GameObject normalCanvas;
    public Text fTime;
    public Text fScore;
    int saveFinalS;
    int saveFinalT;

    void Update()
    {

        //Hp regen for easy mode
        /*
        Hitpoints += Time.deltaTime;
        HitpointsScript.ins.UpdateHP(PlayerStats.Hitpoints);
        */
        
    }

    void Start()
    {
        ins = this;
    }

    public void AddHp (float value)
    {
        if (value + Hitpoints > maxHealth)
        {
            Hitpoints = maxHealth;
        }
        else
        {
            Hitpoints += value;
        }
    }

    protected override void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(false);
        dScreen.SetActive(true);
        normalCanvas.SetActive(false);

        saveFinalS = PlayerStats.Score;
        fScore.text = saveFinalS.ToString();

        saveFinalT = TimeScript.ins.FinalTime();
        fTime.text = TimeScript.ToText(saveFinalT);
        Time.timeScale = 0f;
        print("Pelaaja hävisi!");
        if (SceneManager.GetActiveScene().name == "Survial")
        {
            if (!PlayerPrefs.HasKey("SurvivalTime"))
            {
                PlayerPrefs.SetFloat("SurvivalTime", saveFinalT);
            }
            if (!PlayerPrefs.HasKey("SurvivalScore"))
            {
                PlayerPrefs.SetInt("SurvivalScore", saveFinalS);
            }
            if (saveFinalS > PlayerPrefs.GetInt("SurvivalScore"))
            {
                PlayerPrefs.SetInt("SurvivalScore", saveFinalS);
            }
            if (saveFinalT > PlayerPrefs.GetFloat("SurvivalTime"))
            {
                PlayerPrefs.SetFloat("SurvivalTime", saveFinalT);
            }
        }
    }
}
