using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounterScript : MonoBehaviour
{
    public static KillCounterScript instance;
    int kills;
    private void Start()
    {
        instance = this;
    }
    public void AddKills(int amount)
    {
        kills += amount;
    }
    public int GetKills()
    {
        return kills;
    }
}
