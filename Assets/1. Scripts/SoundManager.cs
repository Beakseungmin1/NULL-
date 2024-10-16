using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgm;
    public AudioSource sfx;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgm.clip != clip)
        {
            bgm.clip = clip;
            bgm.Play();
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfx.clip != clip || !sfx.isPlaying)
        {
            sfx.clip = clip;
            sfx.Play();
        }
    }
}
