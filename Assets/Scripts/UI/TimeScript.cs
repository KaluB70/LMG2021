using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Text text;
    int timePast;
    public static TimeScript ins;

    private void Start()
    {
        ins = this;
    }
    void Update()
    {
        int timeNow = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        if (timeNow != timePast)
        {
            text.text = ToText(timeNow);
            timePast = timeNow;
        }
    }
    public static string ToText(int time)
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
    public int FinalTime()
    {
        return Mathf.FloorToInt(Time.timeSinceLevelLoad);
    }
}
