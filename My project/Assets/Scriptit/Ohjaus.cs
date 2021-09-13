using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjaus : MonoBehaviour

{

    Rigidbody rb;
    public float liikkumisNopeus = 2.5f;
    public float k‰‰ntymisNopeus = 200f;
    public Transform yl‰osa;
    public LayerMask s‰teenVaikutus;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float pystysuora = Input.GetAxisRaw("Vertical");
        Vector3 siirtym‰ = transform.forward * pystysuora * liikkumisNopeus * Time.deltaTime;
        rb.MovePosition(transform.position + siirtym‰);

        float vaakasuora = Input.GetAxisRaw("Horizontal");
        Quaternion k‰‰ntym‰ = Quaternion.Euler(0, vaakasuora * k‰‰ntymisNopeus * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * k‰‰ntym‰);
    }
    // Update is called once per frame
    void Update()
    {
        Ray s‰de = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit osuma;
        if (Physics.Raycast(s‰de, out osuma, 100, s‰teenVaikutus))
        {
            Vector3 katsomissuunta = osuma.point - transform.position;
            katsomissuunta.y = 0;
            yl‰osa.rotation = Quaternion.LookRotation(katsomissuunta);
        }
    }   
}
