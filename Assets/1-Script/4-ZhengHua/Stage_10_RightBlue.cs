using DG.Tweening;
using UnityEngine;

namespace ZhengHua
{
    public class Stage_10_RightBlue : StageManager
    {
        private bool haveKey02 = false;

        [SerializeField] private GameObject key02;
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
            if (haveKey02 == false)
            {
                key02.transform.position = startPos.position;
            }
            
            fishingRod.transform.localRotation = Quaternion.identity;
        }

        public void GetMaskB()
        {
            if (isFishing)
                return;
            
            var fishingRodShake = fishingRod.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0), 20, 90f);
            fishingRodShake.Pause();
            if (haveKey02)
            {
                fishingRodShake.Play();
                return;
            }
            isFishing = true;
                
            var sequence = DOTween.Sequence();
            var maskBTween = key02.transform.DOMove(gotPos.position, 0.5f);
            maskBTween.SetEase(Ease.OutBack);
            maskBTween.Pause();
            
            var fishingRodUp = fishingRod.transform.DOLocalRotate(new Vector3(0f, 0f, -30f), 0.3f);
            fishingRodUp.Pause();
            
            sequence.Append(fishingRodShake);
            sequence.Append(maskBTween);
            sequence.Insert(1, fishingRodUp);
            
            maskBTween.SetEase(Ease.OutBack);
            maskBTween.Pause();
        }

        public void OnMaskBObjectClick()
        {
            var maskBGotTween = key02.transform.DOMove(endPos.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveKey02 = true;
                if (gameManager != null)
                {
                    gameManager.isMaskC_active = true;
                }
            };
        }
    }
}