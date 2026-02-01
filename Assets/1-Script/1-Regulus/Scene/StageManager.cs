using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    [Title("Renderers")]
    [SerializeField] private SkinnedMeshRenderer leftRenderer;
    [SerializeField] private SkinnedMeshRenderer rightRenderer;

    [Title("BlendShape")]
    [SerializeField] private int blendShapeIndex;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float flashDuration = 0.15f; // 快速閃動時間

    // ---------- Buttons ----------

    [Button("Left Open")]
    private void LeftOpen() => Play(leftRenderer, 100f);

    [Button("Left Close")]
    private void LeftClose() => Play(leftRenderer, 0f);

    [Button("Right Open")]
    private void RightOpen() => Play(rightRenderer, 100f);

    [Button("Right Close")]
    private void RightClose() => Play(rightRenderer, 0f);

    [Button("Left Flash")]
    private void LeftFlash() => Flash(leftRenderer);

    [Button("Right Flash")]
    private void RightFlash() => Flash(rightRenderer);

    // ---------- Core ----------

    private void Play(SkinnedMeshRenderer r, float target)
    {
        if (r == null || r.sharedMesh == null) return;
        if (blendShapeIndex < 0 || blendShapeIndex >= r.sharedMesh.blendShapeCount) return;

        float current = r.GetBlendShapeWeight(blendShapeIndex);

        DOTween.Kill(r);
        DOTween.To(
                () => current,
                x =>
                {
                    current = x;
                    r.SetBlendShapeWeight(blendShapeIndex, x);
                },
                target,
                duration)
            .SetTarget(r);
    }

    private void Flash(SkinnedMeshRenderer r)
    {
        if (r == null || r.sharedMesh == null) return;
        if (blendShapeIndex < 0 || blendShapeIndex >= r.sharedMesh.blendShapeCount) return;

        DOTween.Kill(r);

        float current = r.GetBlendShapeWeight(blendShapeIndex);

        // 建立序列：先開 → 再關
        Sequence seq = DOTween.Sequence().SetTarget(r);
        seq.Append(DOTween.To(
                () => current,
                x =>
                {
                    current = x;
                    r.SetBlendShapeWeight(blendShapeIndex, x);
                },
                100f,
                flashDuration));
        seq.Append(DOTween.To(
                () => current,
                x =>
                {
                    current = x;
                    r.SetBlendShapeWeight(blendShapeIndex, x);
                },
                0f,
                flashDuration));
    }
}
