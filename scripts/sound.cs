using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip koloda_sound;
    public AudioClip take_sound;
    public AudioClip put_sound;
    // Start is called before the first frame update

    public void Koloda()
    {
        AudioSource.PlayClipAtPoint(koloda_sound, transform.position);
    }
    public void take()
    {      

       // AudioSource.PlayClipAtPoint(take_sound, transform.position);
    }
    public void put()
    {
        //AudioSource.PlayClipAtPoint(put_sound, transform.position);
    }
}
