using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MaskArtData", menuName = "Mask/MaskArtData")]
public sealed class MaskArtData : ScriptableObject
{
    [TableList] public List<MaskArtStruct> Masks = new();

    /// <summary>
    /// 根據 MaskType 取得對應 Icon
    /// </summary>
    public Sprite GetIcon(MaskType type) =>
        GetValue(Masks, type, x => x.Type, x => x.Icon);

    private TResult GetValue<TData, TKey, TResult>(
        List<TData> list,
        TKey key,
        Func<TData, TKey> keySelector,
        Func<TData, TResult> valueSelector,
        TResult defaultValue = default)
    {
        foreach (var item in list)
            if (EqualityComparer<TKey>.Default.Equals(keySelector(item), key))
                return valueSelector(item);
        return defaultValue;
    }
}

[Serializable]
public class MaskArtStruct
{
    public MaskType Type;

    [PreviewField(96, ObjectFieldAlignment.Center)]
    public Sprite Icon;
}

public enum MaskType
{
    None,
    LeftRed,    // 左紅
    LeftGreen,  // 左綠
    LeftBlue,   // 左藍
    RightRed,   // 右紅
    RightGreen, // 右綠
    RightBlue   // 右藍
}

