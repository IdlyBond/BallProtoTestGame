using System;
using UnityEngine;
using Object = UnityEngine.Object;


public static class SoundManager
{
    public enum Sound
    {
        Coin,
        Bubble,
        BubbleHigh,
        Win,
    }
    
    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObject = new GameObject("Sound");
            soundObject.transform.position = position;
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = GetClip(sound);
            audioSource.Play();
            if (soundObject) Object.Destroy(soundObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObject = new GameObject("Sound");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            AudioClip clip = GetClip(sound);
            audioSource.PlayOneShot(clip);
            Object.Destroy(soundObject, clip.length);
        }
    }
    
    public static void PlaySound(Sound sound, float volume)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObject = new GameObject("Sound");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            AudioClip clip = GetClip(sound);
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip, volume);
            Object.Destroy(soundObject, clip.length);
        }
    }
    public static void PlaySound(Sound sound, float volume, float pitch)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObject = new GameObject("Sound " + sound);
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            AudioClip clip = GetClip(sound);
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip, volume);
            Object.Destroy(soundObject, clip.length);
        }
    }
    

    public static AudioClip GetClip(Sound sound)
    {
        foreach (var sac in GameAssets.i.sounds)
        {
            if (sac.sound == sound) return sac.clip;
        }
        Debug.LogError("Non-Existing Sound");
        return null;
    }

    private static bool CanPlaySound(Sound sound)
    {
        return true;
    }
}

[Serializable]
public class SoundAudioClip
{
    public SoundManager.Sound sound;
    public AudioClip clip;
}