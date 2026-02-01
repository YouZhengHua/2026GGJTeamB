using UnityEngine;
using TMPro;

public class CreditsScroller : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public RectTransform viewportRect;   // Panel_Credits 的 RectTransform
    public RectTransform creditsRect;    // TxtCredits 的 RectTransform
    public TMP_Text creditsText;         // TxtCredits 的 TMP 元件

    [Header("Tuning")]
    public float speed = 60f;            // 每秒向下移動像素
    public float padding = 40f;          // 起始/結束留白
    public bool playOnEnable = true;
    public bool loop = false;

    float startY, endY;
    bool playing;

    void OnEnable()
    {
        if (playOnEnable) Begin();
    }

    public void Begin()
    {
        // 取得文字實際高度，避免高度不準
        if (creditsText != null)
        {
            creditsText.ForceMeshUpdate();
            float h = creditsText.preferredHeight;
            var size = creditsRect.sizeDelta;
            creditsRect.sizeDelta = new Vector2(size.x, h);
        }

        float viewportH = viewportRect.rect.height;
        float contentH  = creditsRect.rect.height;

        // 內容完全在視窗上方（看不到）
        startY = viewportH * 0.5f + contentH * 0.5f + padding;
        // 內容完全在視窗下方（看不到）
        endY   = -(viewportH * 0.5f + contentH * 0.5f + padding);

        var p = creditsRect.anchoredPosition;
        creditsRect.anchoredPosition = new Vector2(p.x, startY);

        playing = true;
    }

    void Update()
    {
        if (!playing) return;

        var p = creditsRect.anchoredPosition;
        // 往下滾：y 變小
        p.y -= speed * Time.unscaledDeltaTime;
        creditsRect.anchoredPosition = p;

        if (p.y <= endY)
        {
            if (loop) Begin();
            else playing = false;
        }
    }
}
