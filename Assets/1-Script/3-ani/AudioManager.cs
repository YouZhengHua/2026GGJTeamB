using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip menuBgm;
    [SerializeField] private AudioClip uiClick;

    [Header("Volume 0~1")]
    [Range(0f, 1f)] [SerializeField] private float bgmVolume = 1f;
    [Range(0f, 1f)] [SerializeField] private float sfxVolume = 1f;

    private const string KEY_MUTE = "mute_all";

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        // 若你忘了指派，嘗試自動抓同物件上的兩個 AudioSource
        if (bgmSource == null || sfxSource == null)
        {
            var sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                // 慣例：第一個當 BGM，第二個當 SFX
                if (bgmSource == null) bgmSource = sources[0];
                if (sfxSource == null) sfxSource = sources[1];
            }
        }
    }

    private void Start()
    {
        ApplyMute(PlayerPrefs.GetInt(KEY_MUTE, 0) == 1);
        PlayMenuBgm();
    }

    public void PlayMenuBgm()
    {
        if (bgmSource == null || menuBgm == null) return;

        bgmSource.clip = menuBgm;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;

        if (!bgmSource.isPlaying) bgmSource.Play();
    }

    public void PlayClick()
    {
        if (sfxSource == null || uiClick == null) return;
        sfxSource.PlayOneShot(uiClick, sfxVolume);
    }

    public void ToggleMuteAll()
    {
        bool muted = PlayerPrefs.GetInt(KEY_MUTE, 0) == 1;
        muted = !muted;

        PlayerPrefs.SetInt(KEY_MUTE, muted ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMute(muted);
    }

    private void ApplyMute(bool muted)
    {
        // 最直觀的全域靜音方式
        AudioListener.pause = muted;
    }
}
