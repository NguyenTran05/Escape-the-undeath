using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager> 
{ 
    private float musicFadeSpeedRate = CONST.Music_FADE_SPEED_RATE_HIGH;

    private string nextMusicName;
    private string nextSoundName;

    private bool isFaseOut = false;

    public AudioSource AttachMusicSource;
    public AudioSource AttachSoundSource;

    private Dictionary<string, AudioClip> musicDic, soundDic;

    protected override void Awake()
    {
        base.Awake();


        //Load all SE and BGM files from resource folder
        musicDic = new Dictionary<string, AudioClip>();
        soundDic = new Dictionary<string, AudioClip>();

        object[] musicList = Resources.LoadAll("Audio/Music");
        object[] soundList = Resources.LoadAll("Audio/Sound");

        foreach (AudioClip music in musicList)
        {
            musicDic[music.name] = music;
        }
        foreach (AudioClip sound in soundList)
        {
            soundDic[sound.name] = sound;
        }
    }
    private void Start()
    {
        AttachMusicSource.volume = PlayerPrefs.GetFloat(CONST.Music_VOLUME_KEY, CONST.Music_VOLUME_DEFAULT);
        AttachSoundSource.volume = PlayerPrefs.GetFloat(CONST.Sound_VOLUME_KEY, CONST.Sound_VOLUME_DEFAULT);
    }

    public void PlaySound(string soundName, float delay = 0.0f)
    {
        if (!soundDic.ContainsKey(soundName))
        {
            Debug.LogError(soundName + "There is no Sound named");
            return;
        }
        nextSoundName = soundName;
        Invoke("DelayPlaySE", delay);
    }
    private void DelayPlaySE()
    {
        AttachSoundSource.PlayOneShot(soundDic[nextSoundName] as AudioClip);
    }
    public void PlayMusic(string musicName, float fadeSpeedRate = CONST.Music_FADE_SPEED_RATE_HIGH)
    {
        if (!musicDic.ContainsKey(musicName))
        {
            Debug.LogError(musicName + "There is no Music named");
            return;
        }
        //BGM is not currently playing
        if (!AttachMusicSource.isPlaying)
        {
            nextMusicName = ""; 
            AttachMusicSource.clip = musicDic[musicName] as AudioClip;
            AttachMusicSource.Play();
        }
        // BGM is playing
        else if (AttachMusicSource.clip.name != musicName)
        {
            nextMusicName = musicName;
            FadeOutMusic(fadeSpeedRate);
        }
    }
    public void FadeOutMusic(float fadeSpeedRate = CONST.Music_FADE_SPEED_RATE_LOW)
    {
        musicFadeSpeedRate = fadeSpeedRate;
        isFaseOut = true;

    }
    private void Update()
    {
        if (!isFaseOut)
        {
            return;
        }

        //Gradually lower the volume, when the volume reaches 0 play the next BGM
        AttachMusicSource.volume -= Time.deltaTime * musicFadeSpeedRate;
        if (AttachMusicSource.volume <= 0)
        {
            AttachMusicSource.Stop();
            AttachMusicSource.volume = PlayerPrefs.GetFloat(CONST.Music_VOLUME_KEY, CONST.Music_VOLUME_DEFAULT);
            isFaseOut = false;
            if (!string.IsNullOrEmpty(nextMusicName))
            {
                PlayMusic(nextMusicName);
            }
        }
    }
    public void ChangeMusicVolume(float MusicVolume)
    {
        AttachMusicSource.volume = MusicVolume;
        PlayerPrefs.SetFloat(CONST.Music_VOLUME_KEY, MusicVolume);
    }
    public void ChangeSoundVolume(float SoundVolume)
    {
        AttachSoundSource.volume = SoundVolume;
        PlayerPrefs.SetFloat(CONST.Sound_VOLUME_KEY, SoundVolume);
    }
}
