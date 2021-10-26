using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Text text;
    int timePast;
    void Update()
    {
        int timeNow = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        if (timeNow != timePast)
        {
            text.text = ToText(timeNow);
            timePast = timeNow;
        }
    }
    private string ToText(int time)
    {
        int hours = time / 3600;
        int minutes = (time % 3600) / 60;
        int seconds = time % 60;
        if (hours >0)
        {
            return string.Format("{0:D2}:{1:D2}{2:D2}", hours, minutes, seconds);
        }
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
