using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    None=-1,
    Title,
    Game,
}

public enum SE
{
    None = -1,
    HitWall,
    Start,
    GetEnemy,
    TimeUp,
    Result,

}

[System.Serializable]
public class SoundVolume
{
    public float bgm = 1.0f;
    public float se = 1.0f;

    public bool mute = false;

    public void Reset()
    {
        bgm = 1.0f;
        se = 1.0f;
        mute = false;
    }
}

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    public SoundVolume volume = new SoundVolume();

    private Dictionary<BGM, string> bgmName = new Dictionary<BGM, string>()
    {
        {BGM.Title,"harunopayapaya" },
        {BGM.Game,"wintercarnival" },
    };
    private Dictionary<SE, string> seName = new Dictionary<SE, string>()
    {
        {SE.HitWall,"powerdown03" },
        {SE.Start,"info-girl1_info-girl1-youkoso1" },
        {SE.GetEnemy,"powerup04" },
        {SE.TimeUp,"info-girl1_info-girl1-syuuryoushimashita1" },
        {SE.Result, "info-girl1_info-girl1-mataasondene1" },
    };

    private AudioClip[] seClips;
    private AudioClip[] bgmClips;

    private Dictionary<string, int> seIndexes = new Dictionary<string, int>();
    private Dictionary<string, int> bgmIndexes = new Dictionary<string, int>();

    const int cNumChannel = 16;
    
    private AudioSource bgmSource;
    private AudioSource[] seSource = new AudioSource[cNumChannel];


    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        gameObject.name = "SoundManager";

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        for (int i = 0; i < seSource.Length; i++)
            seSource[i] = gameObject.AddComponent<AudioSource>();

        seClips = Resources.LoadAll<AudioClip>("Audio/SE");
        bgmClips = Resources.LoadAll<AudioClip>("Audio/BGM");

        for (int i = 0; i < seClips.Length; i++)
            seIndexes[seClips[i].name] = i;

        for (int i = 0; i < bgmClips.Length; i++)
            bgmIndexes[bgmClips[i].name] = i;
    }

    public void PlayBgm(BGM id)
    {
        int index = bgmIndexes[bgmName[id]];
        PlayBgm(index);
    }
    public void PlayBgm(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
            return;

        if (bgmSource.clip == bgmClips[index])
            return;

        bgmSource.Stop();
        bgmSource.clip = bgmClips[index];
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    public void PauseBgm()
    {
        bgmSource.Pause();
    }

    public void UnPauseBgm()
    {
        bgmSource.UnPause();
    }

    //public void FadeOutBgm(float fadeTime = 1f)
    //{
    //    if (!bgmSource.isPlaying)
    //        return;

    //    bgmSource.volume = volume.bgm;
    //    bgmSource.DOFade(0.0f, fadeTime).SetEase(Ease.InCubic);
    //}
    //public void FadeInBgm(float fadeTime = 1f)
    //{
    //    bgmSource.DOFade(volume.bgm, fadeTime).SetEase(Ease.InCubic);
    //}
    //public void MuteBgm(int index=1)
    //{
    //    bgmSource.volume = 0;
    //}


    public void PlaySe(SE id)
    {
        int index = seIndexes[seName[id]];
        PlaySe(index);
    }
    public void PlaySe(int index)
    {
        if (index < 0 || index >= seClips.Length)
            return;

        foreach(AudioSource source in seSource)
        {
            if(!source.isPlaying)
            {
                source.clip = seClips[index];
                source.Play();
                return;
            }
        }
    }

    public void StopSe()
    {
        foreach(AudioSource source in seSource)
        {
            source.Stop();

        }
    }
}
