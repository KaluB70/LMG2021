using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjaus : MonoBehaviour

{

    Rigidbody rb;
    public float liikkumisNopeus = 2.5f;
    public float kaantymisNopeus = 200f;
    public Transform ylaosa;
    public LayerMask sateenVaikutus;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float pystysuora = Input.GetAxisRaw("Vertical");
        Vector3 siirtyma = transform.forward * pystysuora * liikkumisNopeus * Time.deltaTime;
        rb.MovePosition(transform.position + siirtyma);

        float vaakasuora = Input.GetAxisRaw("Horizontal");
        Quaternion kaantyma = Quaternion.Euler(0, vaakasuora * kaantymisNopeus * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * kaantyma);
    }
    // Update is called once per frame
    void Update()
    {
        Ray sade = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit osuma;
        if (Physics.Raycast(sade, out osuma, 100, sateenVaikutus))
        {
            Vector3 katsomissuunta = osuma.point - transform.position;
            katsomissuunta.y = 0;
            ylaosa.rotation = Quaternion.LookRotation(katsomissuunta);
        }
    }   
}
