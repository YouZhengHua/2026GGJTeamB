using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;

public class MaskManager : MonoBehaviour
{
    [SerializeField] private MaskArtData maskArtData;
    [SerializeField] private MaskButtonView[] maskButtons;

    [Header("左右當前面具顯示")]
    [SerializeField] private Image leftMaskImage;
    [SerializeField] private Image rightMaskImage;

    private MaskType _leftMask = MaskType.None;
    private MaskType _rightMask = MaskType.None;

    private HashSet<MaskType> _unlockedMasks = new HashSet<MaskType>();

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
        // 初始化按鈕順序
        for (int i = 0; i < _initOrder.Length && i < maskButtons.Length; i++)
        {
            var type = _initOrder[i];
            var btn = maskButtons[i];
            var icon = maskArtData.GetIcon(type);

            btn.Init(type, icon);
            btn.OnClicked.Subscribe(_ => OnMaskButtonClicked(btn)).AddTo(this);
        }

        // 初始解鎖右藍
        _unlockedMasks.Add(MaskType.RightBlue);

        RefreshUI();
    }

    private void OnMaskButtonClicked(MaskButtonView btn)
    {
        EquipMask(btn.GetMaskType());
        RefreshUI();
    }

    private void EquipMask(MaskType mask)
    {
        if (mask == MaskType.None) return;

        if (mask.ToString().StartsWith("Left"))
        {
            _leftMask = mask;
            AnimateMaskImage(leftMaskImage, mask != MaskType.None ? maskArtData.GetIcon(mask) : null, true);
        }
        else
        {
            _rightMask = mask;
            AnimateMaskImage(rightMaskImage, mask != MaskType.None ? maskArtData.GetIcon(mask) : null, false);
        }


        // 發出面具切換事件
        StageEvents.RaiseMaskChanged(mask);
    }

    private void RefreshUI()
    {
        foreach (var btn in maskButtons)
        {
            var type = btn.GetMaskType();

            // 尚未解鎖的按鈕隱藏
            if (!_unlockedMasks.Contains(type))
            {
                btn.Hide();
                continue;
            }

            // 同邊已裝備的按鈕隱藏
            if ((type.ToString().StartsWith("Left") && _leftMask == type) ||
                (type.ToString().StartsWith("Right") && _rightMask == type))
            {
                btn.Hide();
            }
            else
            {
                btn.Show();
            }
        }
    }

    /// <summary>
    /// DOTween 動畫效果
    /// </summary>
    private void AnimateMaskImage(Image img, Sprite sprite, bool isLeft)
    {
        if (sprite == null)
        {
            img.enabled = false; // 無裝備就隱藏
            return;
        }

        img.sprite = sprite;
        img.enabled = true;

        var rect = img.rectTransform;
        rect.DOKill();
        rect.localScale = Vector3.zero;
        rect.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutQuad);
    }

    // ---------------- Odin 測試用 Button ----------------
    [Button("測試解鎖左紅")]
    private void TestUnlockLeftRed() => UnlockMask(MaskType.LeftRed);

    [Button("測試解鎖右綠")]
    private void TestUnlockRightGreen() => UnlockMask(MaskType.RightGreen);

    /// <summary>
    /// 解鎖碎片
    /// </summary>
    public void UnlockMask(MaskType mask)
    {
        if (!_unlockedMasks.Contains(mask))
            _unlockedMasks.Add(mask);

        RefreshUI();
    }

    public MaskType GetEquippedLeft() => _leftMask;
    public MaskType GetEquippedRight() => _rightMask;
}
