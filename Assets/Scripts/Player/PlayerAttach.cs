using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttach : MonoBehaviour 
{
    public GameObject Player;

    //Jos liikkuvan alustan trigger box collaideriin osuu
    //Player objekti, liitetaan se platformiin samaksi elementiksi
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    //irroitetaan Player objekti liikkuvasta alustasta
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

}
