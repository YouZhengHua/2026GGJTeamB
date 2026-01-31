using DG.Tweening;
using UnityEngine;

namespace ZhengHua
{
    public class MaskAStage : StageManager
    {
        private bool haveMaskB = false;

        [SerializeField] private GameObject maskB;
        [SerializeField] private GameObject fishingRod;

        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        [SerializeField] private Transform gotPos;
        
        private bool isFishing = false;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        public override void StageInit()
        {
            base.StageInit();

            isFishing = false;
            
            // 還未拿到碎片 B 重置碎片位置。
            if (haveMaskB == false)
            {
                maskB.transform.position = startPos.position;
            }
            
            fishingRod.transform.localRotation = Quaternion.identity;
        }

        public void GetMaskB()
        {
            if (isFishing)
                return;
            
            var fishingRodShake = fishingRod.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0), 20, 90f);
            fishingRodShake.Pause();
            if (haveMaskB)
            {
                fishingRodShake.Play();
                return;
            }
            isFishing = true;
                
            var sequence = DOTween.Sequence();
            var maskBTween = maskB.transform.DOMove(gotPos.position, 0.5f);
            maskBTween.SetEase(Ease.OutBack);
            maskBTween.Pause();
            
            var fishingRodUp = fishingRod.transform.DOLocalRotate(new Vector3(0f, 0f, 30f), 0.3f);
            fishingRodUp.Pause();
            
            sequence.Append(fishingRodShake);
            sequence.Append(maskBTween);
            sequence.Insert(1, fishingRodUp);
            
            maskBTween.SetEase(Ease.OutBack);
            maskBTween.Pause();
        }

        public void OnMaskBObjectClick()
        {
            var maskBGotTween = maskB.transform.DOMove(endPos.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveMaskB = true;
                if (gameManager != null)
                {
                    gameManager.isMaskB_active = true;
                }
            };
        }
    }
}