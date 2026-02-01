using UnityEngine;
using TMPro;

public class CreditsScroller : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public RectTransform viewportRect;   // Panel_Credits RectTransform
    public RectTransform creditsRect;    // TxtCredits RectTransform
    public TMP_Text creditsText;         // TxtCredits TMP_Text (TextMeshProUGUI)

    [Header("Tuning")]
    public float speed = 60f;            // pixels/sec
    public float padding = 40f;          // extra spacing
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
        if (viewportRect == null || creditsRect == null) return;

        // Ensure correct content height
        if (creditsText != null)
        {
            creditsText.ForceMeshUpdate();
            float h = creditsText.preferredHeight;
            var size = creditsRect.sizeDelta;
            creditsRect.sizeDelta = new Vector2(size.x, h);
        }

        float viewportH = viewportRect.rect.height;
        float contentH = creditsRect.rect.height;

        // Start: fully above viewport
        startY = viewportH * 0.5f + contentH * 0.5f + padding;
        // End: fully below viewport
        endY = -(viewportH * 0.5f + contentH * 0.5f + padding);

        var p = creditsRect.anchoredPosition;
        creditsRect.anchoredPosition = new Vector2(p.x, startY);

        playing = true;
    }

    void Update()
    {
        if (!playing) return;

        var p = creditsRect.anchoredPosition;
        p.y -= speed * Time.unscaledDeltaTime; // move down
        creditsRect.anchoredPosition = p;

        if (p.y <= endY)
        {
            if (loop) Begin();
            else playing = false;
        }
    }
}
