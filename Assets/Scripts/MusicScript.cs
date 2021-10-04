using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] songs;
    AudioSource audioSrc;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
    }
    private void Update()
    {
        if (!audioSrc.isPlaying)
        {
            PlaySong();
        }
    }
    private void PlaySong()
    {
        audioSrc.Stop();
        audioSrc.clip = songs[Random.Range(0, songs.Length)];
        audioSrc.Play();
    }
}
