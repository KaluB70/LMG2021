using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject UIHandler;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        TutorialEnemySpawner spawner = GetComponentInParent<TutorialEnemySpawner>();
        UIHandler.GetComponent<TutorialUIScript>().setActive = true;
        spawner.setActive = true;
    }
}
