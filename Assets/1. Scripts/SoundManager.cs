using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  �ӽ�Bgm�Դϴ�.
public enum Bgm
{
    TitleBgm,
    GameBgm,
    BossBgm
}

//   �׽�Ʈ�� ȿ�����Դϴ�.
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

    private AudioSource bgmSource;  // BGM �����
    private AudioSource sfxSource;  // SFX �����
    private float volume = 0.5f;    // �ʱ� ���� ����

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
        // �׽�Ʈ��
        SoundManager.instance.PlayBGM(Bgm.TitleBgm);
        //
        volumeSlider.gameObject.SetActive(false);
        soundButton.GetComponent<Button>().onClick.AddListener(ToggleSlider);  //���� ��ư Ŭ�� (�����̵� Ȱ/��Ȱ��ȭ)

        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // �����̴��� Ȱ��ȭ ����
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
                bgmSource.loop = loop;  // BGM �ݺ� ���, �ʿ������ ����
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

    // �����̴��� ��ü ���� ����
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        bgmSource.volume = volume;
    }
}
