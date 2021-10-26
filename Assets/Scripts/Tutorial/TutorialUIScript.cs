using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIScript : MonoBehaviour
{
    public static TutorialUIScript instance;
    public GameObject enemyTutorial;
    public Text desciption;
    public Text enemyName;
    public Image img;
    public Text statsField;
    public bool setActive = false;
    public Sprite[] enemySprites;
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (setActive)
        {
            enemyTutorial.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = setActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
    public void setEnemyInfoNormal ()
    {
        enemyName.text = "Normal enemy";
        desciption.text = "Normal enemy has balanced stats";
        statsField.text ="Melee \n20 \n100 \n100";
        img.sprite = enemySprites[0];
    }
    public void setEnemyInfoTank()
    {
        enemyName.text = "Tank";
        desciption.text = "Tank has higher health but is slower and does less damage";
        statsField.text = "Melee \n10 \n200 \n125";
        img.sprite = enemySprites[0];
    }
    public void setEnemyInfoFast()
    {
        enemyName.text = "Fast";
        desciption.text = "Fast is faster than the others, but also weaker";
        statsField.text = "Melee \n20 \n50 \n100";
        img.sprite = enemySprites[0];
    }
    public void setEnemyInfoFlying()
    {
        enemyName.text = "Flying";
        desciption.text = "Flying enemies are weak, but annoying";
        statsField.text = "Melee \n10 \n75 \n150";
        img.sprite = enemySprites[0];
    }
    public void Continue()
    {
        setActive = false;
        enemyTutorial.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = setActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
