using DG.Tweening;
using UnityEngine;

namespace ZhengHua
{
    /// <summary>
    /// 左藍
    /// 北極熊, 冰山, 寶藏(End Point)
    /// 
    /// 流程十三
    /// </summary>
    public class Stage_01_LeftBlue : StageManager
    {
        [SerializeField] private GameObject bear;
        [SerializeField] private Stage_10_RightRed rightRed;
        private bool _isBearOut = false;

        public override void StageInit()
        {
            base.StageInit();

            if (rightRed.HaveCookedFish && this.IsRightRedOnCurrent)
            {
                bear.transform.DOMoveX(3.86f, 1.5f).OnComplete(() =>
                {
                    _isBearOut = true;
                    bear.transform.SetParent(rightRed.transform);
                });
            }
            
            AudioManager.Instance.PlayBGM("Ocean_Wave_Music");
        }

        public void GotEnd()
        {
            if (!_isBearOut)
                return;
            Debug.Log("IS END.");
        }
    }
}