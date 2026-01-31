using DG.Tweening;
using UnityEngine;

namespace ZhengHua
{
    /// <summary>
    /// 左紅
    /// 流程二
    /// 點擊太陽拉繩釣出碎片左綠 key01
    /// </summary>
    public class Stage_02_LeftRed : StageManager
    {
        private bool haveKey01 = false;

        [SerializeField] private GameObject key01;
        [SerializeField] private GameObject lightToggle;

        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        [SerializeField] private Transform gotPos1;
        [SerializeField] private Transform gotPos2;
        
        private bool isLightOpened = false;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        public override void StageInit()
        {
            base.StageInit();

            isLightOpened = false;
            
            // 還未拿到碎片 B 重置碎片位置。
            if (haveKey01 == false)
            {
                key01.transform.position = startPos.position;
            }
            
            lightToggle.transform.localRotation = Quaternion.identity;
        }

        public void FindKey01()
        {
            if (isLightOpened)
                return;
            
            var lightToggleSequence = DOTween.Sequence();
            var lightToggleUp = lightToggle.transform.DOMoveY(lightToggle.transform.position.y - 1f, 0.3f);
            lightToggleUp.Pause();
            var lightToggleDown = lightToggle.transform.DOMoveY(lightToggle.transform.position.y, 0.2f);
            lightToggleUp.Pause();
            lightToggleSequence.Append(lightToggleUp);
            lightToggleSequence.Append(lightToggleDown);
            lightToggleSequence.Pause();
            if (haveKey01)
            {
                lightToggleSequence.Play();
                return;
            }
            
            isLightOpened = true;
            
            var key01Down = key01.transform.DOMove(gotPos1.position, 0.5f);
            key01Down.Pause();
            var key01End = key01.transform.DOMove(gotPos2.position, 1.5f);
            key01End.Pause();
            
            lightToggleSequence.Append(key01Down);
            lightToggleSequence.Append(key01End);
            lightToggleSequence.Play();
        }

        public void OnKey01ObjectClick()
        {
            var maskBGotTween = key01.transform.DOMove(endPos.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveKey01 = true;
                if (gameManager != null)
                {
                    gameManager.isMaskB_active = true;
                }
            };
        }
    }
}