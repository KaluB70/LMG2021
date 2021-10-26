using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public GameObject pfDmgPopup;
   // public static DamagePopup Create()
   // {
        //Transform dmgPopupTransform = Instantiate(pfDmgPopup, Vector3.zero, Quaternion.identity);
    //}
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(float damage)
    {
        textMesh.SetText(Mathf.Floor(damage).ToString());
    }
}
