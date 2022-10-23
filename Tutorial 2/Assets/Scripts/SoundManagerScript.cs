using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip winMusic;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        winMusic = Resources.Load<AudioClip>("Win sound");

        audioSrc = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Win sound":
                audioSrc.PlayOneShot(winMusic);
                break;
        }
    }
}