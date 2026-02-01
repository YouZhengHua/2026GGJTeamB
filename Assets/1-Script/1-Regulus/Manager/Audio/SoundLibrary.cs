using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    [SerializeField] private List<AudioClip> clips;

    private Dictionary<string, AudioClip> _cache;

    private void OnEnable()
    {
        _cache = new Dictionary<string, AudioClip>();
        foreach (var clip in clips)
        {
            if (clip == null) continue;
            if (!_cache.ContainsKey(clip.name))
                _cache.Add(clip.name, clip);
        }
    }

    public AudioClip Get(string clipName)
    {
        if (_cache != null && _cache.TryGetValue(clipName, out var clip))
            return clip;

        return null;
    }
}
