using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsUIScript : MonoBehaviour
{
    public static BuffsUIScript ins;
    public Text time;
    public Text title;
    public GameObject buffWindow;
    public Sprite [] images; //Buff spritet
    public Image image;
    bool active = false;
    string pickupName;
    float dur;

    private void Start()
    {
        image = buffWindow.GetComponentInChildren<Image>();
        ins = this;
    }
    private void Update()
    {
        if (active)
        {
            buffWindow.SetActive(true);
            title.text = pickupName;
            
            dur -= Time.deltaTime;
            if (dur > 0)
            {
                time.text = dur.ToString("00");
            }
            else
            {
                buffWindow.SetActive(false);
                active = false;
            }
        }
    }
    public void SetBuff(string buffName, float durationInSec)
    {
        switch (buffName)
        {
            case "Rapid Fire":
                image.sprite = images[0];
                break;
            case "Higher Caliber":
                image.sprite = images[1];
                break;
            default:
                break;
        }
        pickupName = buffName;
        dur = durationInSec;
        active = true;
    }
    public bool CheckActive()
    {
        if (active)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
