using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgm;
    public AudioSource sfx;

    public Slider volumeSlider;
    public GameObject soundButton;

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

    private void Start()
    {
        volumeSlider.gameObject.SetActive(false);
        soundButton.GetComponent<Button>().onClick.AddListener(ToggleSlider);  //사운드 버튼 클릭

        if (volumeSlider != null)
        {
            volumeSlider.value = bgm.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }
    public void ToggleSlider()
    {
        // 슬라이더의 활성화 상태
        bool isActive = volumeSlider.gameObject.activeSelf;
        volumeSlider.gameObject.SetActive(!isActive);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgm.clip != clip)
        {
            bgm.clip = clip;
            bgm.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfx.clip != clip || !sfx.isPlaying)
        {
            sfx.clip = clip;
            sfx.Play();
        }
    }

    public void SetVolume(float volume)
    {
        bgm.volume = volume;
        sfx.volume = volume;
    }
}
