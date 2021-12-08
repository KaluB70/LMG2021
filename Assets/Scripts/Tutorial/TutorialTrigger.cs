using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject guideArrow;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<TutorialEnemySpawner>().setActive = true;
            TutorialUIScript.instance.Open();
            gameObject.SetActive(false);
            guideArrow.SetActive(false);
        }
    }
}
