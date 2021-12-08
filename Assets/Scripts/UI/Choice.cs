using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Choice : MonoBehaviour
{
    public static Choice ins;
    public string[] buffNames = { "+25 Damage", "+75 Health", "+2 Movement Speed",
            "+3 Jump Height", "-10% Damage Taken", "*1.4 Fire Rate",
            "+5 Seconds Pickup Buff Duration",
            "+2 Sprint Speed", "-15% Gravity", "+50 Damage", "+125 Health", "+5 Movement Speed",
            "+6 Jump Height", "-15% Damage Taken", "*1.2 Fire Rate",
            "+5 Sprint Speed", "-10% Gravity", "+10 Seconds Pickup Buff Duration", "+4 Health/Kill", "+8 Health/Kill",
            "+30 Hookshot Range", "-1 Seconds Hookshot Cooldown", "+20 Hookshot Range", "-2 Seconds Hookshot Cooldown"};

    private void Start()
    {
        ins = this;
    }
    public void ShowPopup(GameObject door)
    {
        string[] choices = BuffPicker();
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("UIHandler").GetComponent<PauseMenuScript>().enabled = false;
        SimpleModalWindow.Create(ignorable: false)
                    .SetHeader("Choose An Upgrade")
                    .SetBody("Think twice - The one you do not choose will be discarded until restart...")
                    .AddButton(choices[0], () =>
                    {
                        BuffHandler(choices[0]);
                        Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;
                        this.gameObject.SetActive(false);
                        door.SetActive(false);
                        GameObject.Find("UIHandler").GetComponent<PauseMenuScript>().enabled = true;
                    },
                    ModalButtonType.Normal)
                    .AddButton(choices[1], () =>
                    {
                        BuffHandler(choices[1]);
                        Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;
                        this.gameObject.SetActive(false);
                        door.SetActive(false);
                        GameObject.Find("UIHandler").GetComponent<PauseMenuScript>().enabled = true;
                    },
                    ModalButtonType.Normal)
                    .Show();
        Invoke("SetTimeScale0", 0.3f);
    }
    private void SetTimeScale0()
    {
        Time.timeScale = 0;
    }
    private string[] BuffPicker()
    {
        string[] retVal = new string[2];
        int chosenBuff = Random.Range(0, buffNames.Length-1);
        for (int i = 0; i < buffNames.Length; i++)
        {
            if (buffNames[chosenBuff] == buffNames[i])
            {
                retVal[0] = buffNames[i];
            }
        }
        buffNames = buffNames.Except(new string[] { buffNames[chosenBuff] }).ToArray();
        int chosenBuff2;
        do
        {
            chosenBuff2 = Random.Range(0, buffNames.Length-1);
        } while (chosenBuff == chosenBuff2);

        for (int i = 0; i < buffNames.Length; i++)
        {
            if (buffNames[chosenBuff2] == buffNames[i])
            {
                retVal[1] = buffNames[i];
            }
        }
        buffNames = buffNames.Except(new string[] { buffNames[chosenBuff2] }).ToArray();
        return retVal;
    }
    private void BuffHandler(string name)
    {
        switch (name)
        {
            case "+25 Damage":
                WeaponScript.ins.damage += 25;
                break;
            case "+75 Health":
                PlayerDeath.ins.maxHealth += 75.0f;
                PlayerDeath.ins.AddHp(75.0f);
                HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);
                break;
            case "+2 Movement Speed":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().characterSpeed += 2f;
                break;
            case "+3 Jump Height":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().jumpHeight += 3f;
                break;
            case "-10% Damage Taken":
                PlayerDeath.ins.damageMultiplier -= 0.1f;
                break;
            case "*1.4 Fire Rate":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponScript>().fireRate *= 1.4f;
                break;
            case "+2 Sprint Speed":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().sprintSpeed += 2f ;
                break;
            case "-15% Gravity":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gravity *= 0.85f;
                break;
            case "+50 Damage":
                WeaponScript.ins.damage += 50;
                break;
            case "+125 Health":
                PlayerDeath.ins.maxHealth += 125f;
                PlayerDeath.ins.AddHp(125f);
                HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);
                break;
            case "+5 Movement Speed":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().characterSpeed += 5;
                break;
            case "+6 Jump Height":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().jumpHeight += 6;
                break;
            case "-15% Damage Taken":
                PlayerDeath.ins.damageMultiplier -= 0.15f;
                break;
            case "*1.2 Fire Rate":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponScript>().fireRate *= 1.2f;
                break;
            case "+5 Sprint Speed":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().sprintSpeed += 5f;
                break;
            case "-10% Gravity":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gravity *= 0.9f;
                break;
            case "+10 Seconds Pickup Buff Duration":
                Burstfire.duration += 10f;
                Pickup_CalUp.duration += 10f;
                break;
            case "+5 Seconds Pickup Buff Duration":
                Burstfire.duration += 5f;
                Pickup_CalUp.duration += 5f;
                break;
            case "+4 Health/Kill":
                EnemyDeath.EnableLifeSteal(4.0f);
                break;
            case "+8 Health/Kill":
                EnemyDeath.EnableLifeSteal(8.0f);
                break;
            case "+30 Hookshot Range":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().hookshotDistance += 30f;
                break;
            case "+20 Hookshot Range":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().hookshotDistance += 20f;
                break;
            case "-2 Seconds Hookshot Cooldown":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().cooldownAmount -= 2f;
                break;
            case "-1 Seconds Hookshot Cooldown":
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().cooldownAmount -= 1f;
                break;
            default:
                break;
            
        }
    }
}
