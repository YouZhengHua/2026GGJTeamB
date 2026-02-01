using UniRx;
using UnityEngine;

public static class StageEvents
{
    // 當面具切換時發出事件
    public static readonly Subject<MaskType> OnMaskChanged = new Subject<MaskType>();

    // 發送事件
    public static void RaiseMaskChanged(MaskType mask)
    {
        OnMaskChanged.OnNext(mask);
    }
}
