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

    public Slider volumeSlider;
    public GameObject soundButton;

    private AudioSource bgmSource;  // BGM 재생
    private AudioSource sfxSource;  // SFX 재생
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
        soundButton.GetComponent<Button>().onClick.AddListener(ToggleSlider);  //사운드 버튼 클릭 (슬라이더 활성/비활성화)

        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // 볼륨 슬라이더 활성화 토글
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
                bgmSource.loop = loop;  // BGM 반복 여부, 필요에 따라 설정
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

    // 볼륨 슬라이더 값 변경 처리
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        bgmSource.volume = volume;
    }
}
