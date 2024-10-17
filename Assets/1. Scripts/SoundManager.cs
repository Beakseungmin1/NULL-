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
    WinSfx,
    LoseSfx,
    HitSfx
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    public Slider volumeSlider;
    public GameObject soundButton;

    private AudioSource bgmSource;  // BGM 재생용
    private AudioSource sfxSource;  // SFX 재생용
    private float volume = 0.5f;    // 초기 볼륨 설정

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
        //
        volumeSlider.gameObject.SetActive(false);
        soundButton.GetComponent<Button>().onClick.AddListener(ToggleSlider);  //사운드 버튼 클릭 (슬라이드 활/비활성화)

        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // 슬라이더의 활성화 상태
    public void ToggleSlider()
    {
        bool isActive = volumeSlider.gameObject.activeSelf;
        volumeSlider.gameObject.SetActive(!isActive);
    }

    public void PlayBGM(Bgm bgm, bool loop = true)
    {
        int clipIndex = (int)bgm;
        if (bgmClips.Length > clipIndex)
        {
            if (bgmSource.clip != bgmClips[clipIndex])
            {
                bgmSource.clip = bgmClips[clipIndex];
                bgmSource.volume = volume;
                bgmSource.loop = loop;  // BGM 반복 재생, 필요없으면 수정
                bgmSource.Play();
            }
        }
    }

    public void PlaySFX(Sfx sfx)
    {
        int clipIndex = (int)sfx;
        if (sfxClips.Length > clipIndex)
        {
            sfxSource.PlayOneShot(sfxClips[clipIndex], volume);
        }
    }

    // 슬라이더로 전체 볼륨 조절
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        bgmSource.volume = volume;
    }
}
