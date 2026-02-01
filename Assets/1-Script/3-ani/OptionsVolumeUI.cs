using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsVolumeUI : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [Header("Master (0-100)")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private TMP_Text txtMasterValue;
    [SerializeField] private string paramMaster = "MasterVolume";

    [Header("BGM (0-100)")]
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private TMP_Text txtBGMValue;
    [SerializeField] private string paramBGM = "BGMVolume";

    [Header("SFX (0-100)")]
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private TMP_Text txtSFXValue;
    [SerializeField] private string paramSFX = "SFXVolume";

    private const string KMaster = "opt_master_0_100";
    private const string KBgm = "opt_bgm_0_100";
    private const string KSfx = "opt_sfx_0_100";

    private void Awake()
    {
        InitSlider(sliderMaster, txtMasterValue, KMaster, 80, v => SetVolume(paramMaster, v));
        InitSlider(sliderBGM, txtBGMValue, KBgm, 80, v => SetVolume(paramBGM, v));
        InitSlider(sliderSFX, txtSFXValue, KSfx, 80, v => SetVolume(paramSFX, v));
    }

    private void InitSlider(Slider s, TMP_Text t, string key, int def, System.Action<int> apply)
    {
        if (!s) return;

        s.minValue = 0;
        s.maxValue = 100;
        s.wholeNumbers = true;

        int v = Mathf.Clamp(PlayerPrefs.GetInt(key, def), 0, 100);

        s.SetValueWithoutNotify(v);
        if (t) t.text = v.ToString();
        apply(v);

        s.onValueChanged.AddListener(val =>
        {
            int vv = Mathf.RoundToInt(val);
            if (t) t.text = vv.ToString();
            apply(vv);
            PlayerPrefs.SetInt(key, vv);
            PlayerPrefs.Save();
        });
    }

    private void SetVolume(string exposedParam, int v0to100)
    {
        float linear = Mathf.Clamp(v0to100 / 100f, 0f, 1f);
        float db = (linear <= 0.0001f) ? -80f : Mathf.Log10(linear) * 20f;
        mixer.SetFloat(exposedParam, db);
    }
}
