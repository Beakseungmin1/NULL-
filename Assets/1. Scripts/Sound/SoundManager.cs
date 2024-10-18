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
    DoubleJumpSfx,
    ItemSfx,
    HitSfx
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private AudioSource bgmSource;  // BGM 재생
    private AudioSource sfxSource;  // SFX 재생
    private float bgmVolume = 0.3f; // BGM 초기 볼륨 설정
    private float sfxVolume = 0.3f; // SFX 초기 볼륨 설정

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 테스트용
        SoundManager.instance.PlayBGM(Bgm.TitleBgm);

        // BGM 볼륨 슬라이더 설정
        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.value = bgmVolume;
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        // SFX 볼륨 슬라이더 설정
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    // BGM 볼륨 설정
    public void SetBGMVolume(float newVolume)
    {
        bgmVolume = newVolume;
        bgmSource.volume = bgmVolume;
    }

    // SFX 볼륨 설정
    public void SetSFXVolume(float newVolume)
    {
        sfxVolume = newVolume;
    }

    public void PlayBGM(Bgm bgm, bool loop = true)
    {
        int clipIndex = (int)bgm;
        if (bgmClips.Length > clipIndex)
        {
            if (bgmSource.clip != bgmClips[clipIndex])
            {
                bgmSource.clip = bgmClips[clipIndex];
                bgmSource.volume = bgmVolume;
                bgmSource.loop = loop;  // BGM 반복 여부, 필요에 따라 설정
                                        // ex1 ) 보스가 등장할 때 loop를 false로 설정하여 일회성 재생
                                        // SoundManager.instance.PlayBGM(Bgm.BossBgm, false);
                                        // 타이틀 화면에서 배경음악을 무한 반복 재생
                                        // ex2 ) SoundManager.instance.PlayBGM(Bgm.TitleBgm, true);
                bgmSource.Play();
            }
        }
    }

    public void PlaySFX(Sfx sfx)
    {
        int clipIndex = (int)sfx;
        if (sfxClips.Length > clipIndex)
        {
            sfxSource.PlayOneShot(sfxClips[clipIndex], sfxVolume);  // SFX 볼륨 설정 적용
        }
    }
}
