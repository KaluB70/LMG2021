using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seuranta : MonoBehaviour
    
{
    public Transform kohde;
    Vector3 et�isyys;

    // Start is called before the first frame update
    void Start()
    {
        et�isyys = transform.position - kohde.position;
    }

    void LateUpdate ()
    {
        transform.position = kohde.position + et�isyys;
    }

}
