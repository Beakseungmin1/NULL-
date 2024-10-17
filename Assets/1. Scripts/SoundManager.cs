using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  임시Bgm입니다.
public enum Bgm
{
    TitleBgm,
    GameBgm,
    BossBgm
}

//   테스트용 효과음입니다.
public enum Sfx 
{
    JumpSfx,
    PopSfx
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //  수정 이후 삭제 예정
    public AudioSource bgm;
    public AudioSource sfx;
    //
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

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

    public void PlaySFX(int clipIndex)
    {
        AudioClip clip = sfxClips[clipIndex];
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
