using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MaskButtonView : MonoBehaviour
{
    [SerializeField] private Image maskImage;
    [SerializeField] private Button button;

    private Subject<Unit> _onClicked = new Subject<Unit>();
    public IObservable<Unit> OnClicked => _onClicked;

    private MaskType _maskType;

    public void Init(MaskType maskType, Sprite sprite)
    {
        _maskType = maskType;
        maskImage.sprite = sprite;
        gameObject.SetActive(false); // 預設隱藏，解鎖才顯示
        button.interactable = true;

        button.OnClickAsObservable()
              .Subscribe(_ =>
              {
                  _onClicked.OnNext(Unit.Default);
                  Hide(); // 點擊後關閉互動
              }).AddTo(this);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public MaskType GetMaskType() => _maskType;
}
