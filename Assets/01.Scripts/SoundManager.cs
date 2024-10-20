using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//  임시Bgm입니다.
public enum Bgm
{
    TitleBgm,
    Stage1Bgm,
    Stage2Bgm,
    Stage3Bgm,
    MultiBgm
}

//   S_는 Story / M_은 Multi / Hit(뭐 맞는 사운드)공용
public enum Sfx
{
    S_JumpSfx,
    S_DoubleJumpSfx,
    HitSfx,
    M_FruitSfx,    // 나무에서 과일 생성
    M_EatFruitSfx, // 과일 먹을 때
    M_AttackSfx,
    M_JumpSfx,
    M_Winnersfx,
    UI_BtnClick
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
    private float sfxVolume = 0.7f; // SFX 초기 볼륨 설정

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode Mode)
    {
        switch (scene. name)
        {
            case "TitleScene":
                PlayBGM(Bgm.TitleBgm);
                break;
            case "StageScene1":
                PlayBGM(Bgm.Stage1Bgm);
                break;
            case "StageScene2":
                PlayBGM(Bgm.Stage2Bgm);
                break;
            case "CoopScene":
                PlayBGM(Bgm.MultiBgm);
                break;
            default:
                break;
        }
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
                ///// 일회성 재생 bgm 추가 시 유지, 아니면 이후 삭제 /////
                bgmSource.loop = loop;  // BGM 반복 여부, 필요에 따라 설정
                                        // ex1 ) 보스가 등장할 때 loop를 false로 설정하여 일회성 재생
                                        // SoundManager.instance.PlayBGM(Bgm.BossBgm, false);
                                        // 타이틀 화면에서 배경음악을 무한 반복 재생 // 근데 기본이 루프로 되어있을 것 
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