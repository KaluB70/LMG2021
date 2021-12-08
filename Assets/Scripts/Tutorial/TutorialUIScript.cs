using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIScript : MonoBehaviour
{
    public static TutorialUIScript instance;
    public GameObject enemyTut;
    public Text desciption;
    public Text enemyName;
    public Image img;
    public Text statsField;
    public Sprite[] enemySprites;
    bool active = false;
    void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            instance.enemyTut.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        }
            
    }
    public void setEnemyInfoNormal()
    {
        enemyName.text = "Slimgremus";
        desciption.text = "Common enemy in dungeon. Slow and weak, but keep your distance or you get slimed.";
        statsField.text = "Melee \n20 \n200 \n100";
        img.sprite = enemySprites[0];
    }
    public void setEnemyInfoTank()
    {
        enemyName.text = "Rockglom";
        desciption.text = "Slow and big golem with sturdy body. Takes time to destroy";
        statsField.text = "Melee \n20 \n1000 \n125";
        img.sprite = enemySprites[1];
    }

    public void setEnemyInfoFast()
    {
        enemyName.text = "Snakredus";
        desciption.text = "Stay vigilant or this sneaky fast snake will bite you. Easy to kill but hard to avoid";
        statsField.text = "Melee \n20 \n110 \n100";
        img.sprite = enemySprites[2];
    }

    public void setEnemyInfoFlying()
    {
        enemyName.text = "Batdremon";
        desciption.text = "This bat is annoying little flyer who shoots lasers at you in close distance.";
        statsField.text = "Melee \n15 \n100 \n75";
        img.sprite = enemySprites[3];
    }
    public void Close()
    {
        instance.enemyTut.SetActive(false);
        instance.active = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void Open()
    {
        instance.active = true;
    }
}
