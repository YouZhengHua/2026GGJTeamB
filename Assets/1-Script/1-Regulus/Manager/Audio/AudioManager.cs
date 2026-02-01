using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [Header("Library")]
    [SerializeField] private SoundLibrary soundLibrary;

    [Header("AudioSource")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public void PlayBGM(string clipName, bool loop = true)
    {
        var clip = soundLibrary.Get(clipName);
        if (clip == null) return;
        if (bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void PlaySFX(string clipName)
    {
        var clip = soundLibrary.Get(clipName);
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
}
