using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UniRx;

public class StageManager : MonoBehaviour
{
    [Title("Renderers")]
    [SerializeField] private SkinnedMeshRenderer leftRenderer;
    [SerializeField] private SkinnedMeshRenderer rightRenderer;

    [Title("BlendShape")]
    [SerializeField] private int blendShapeIndex;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float flashDuration = 0.15f;

    [SerializeField, Title("Mask Objects")]
    private GameObject[] maskObjects; // 順序對應 MaskType[] _initOrder


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

    private MaskType[] _initOrder =
{
    MaskType.LeftRed,
    MaskType.LeftGreen,
    MaskType.LeftBlue,
    MaskType.RightRed,
    MaskType.RightGreen,
    MaskType.RightBlue
};

    private void Start()
    {
        StageEvents.OnMaskChanged.Subscribe(mask =>
        {
            // 找出對應索引
            int index = System.Array.IndexOf(_initOrder, mask);
            if (index < 0 || index >= maskObjects.Length) return;

            bool isLeft = mask.ToString().StartsWith("Left");

            // 先關閉同邊所有物件
            for (int i = 0; i < maskObjects.Length; i++)
            {
                if (maskObjects[i] == null) continue;

                bool objIsLeft = _initOrder[i].ToString().StartsWith("Left");
                if (objIsLeft == isLeft)
                    maskObjects[i].SetActive(false);
            }

            // 再啟用新物件
            var obj = maskObjects[index];
            obj.SetActive(true);

            // 播放簾幕動畫
            if (isLeft)
                Flash(leftRenderer);
            else
                Flash(rightRenderer);
        }).AddTo(this);

    }


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

        // 建立序列：先關 → 再開
        Sequence seq = DOTween.Sequence().SetTarget(r);

        // 先關（0）
        seq.Append(DOTween.To(
            () => current,
            x =>
            {
                current = x;
                r.SetBlendShapeWeight(blendShapeIndex, x);
            },
            0f,
            flashDuration));

        // 再開（100）
        seq.Append(DOTween.To(
            () => current,
            x =>
            {
                current = x;
                r.SetBlendShapeWeight(blendShapeIndex, x);
            },
            100f,
            flashDuration));
    }

}
