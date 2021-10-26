using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsScript : MonoBehaviour
{
    public Text text;
    void Update()
    {
        text.text = PlayerStats.Score.ToString();
    }
}
