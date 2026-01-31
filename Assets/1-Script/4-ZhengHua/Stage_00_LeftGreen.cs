using DG.Tweening;
using Mono.CSharp;
using UnityEngine;

namespace ZhengHua
{
    /// <summary>
    /// 左綠
    /// 風扇, 操作桿, 山坡
    /// 
    /// 流程三
    /// 點擊風扇可以讓葉片轉動
    /// 點擊操作桿可以調整風扇方向
    /// 調整方向後出現碎片右綠 key12
    ///
    /// 流程五
    /// 流程四結束後，可以透過風扇讓右藍的白雲飄到左綠
    ///
    /// 流程六
    /// 流程五結束後，將右邊轉為右綠，並再次點擊風扇，可以將雲朵吹至右綠
    /// </summary>
    public class Stage_00_LeftGreen : StageManager
    {
        private bool haveKey12 = false;

        [SerializeField] private GameObject key12;
        [SerializeField] private GameObject fansToggle;
        [SerializeField] private GameObject fans;
        [SerializeField] private GameObject fansBlade;
        
        [SerializeField] private Transform endPos;
        
        private bool isFansOpen = false;
        private bool isFansTurn = false;

        private GameManager gameManager;
        
        private GameObject _cloud;
        public GameObject CloudGameObject => _cloud;
        [SerializeField] private Stage_11_RightBlue stage_11_rightBlue;
        [SerializeField] private Stage_12_RightGreen stage_12_rightGreen;

        private void Start()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        public override void StageInit()
        {
            base.StageInit();

            isFansOpen = false;
            isFansTurn = false;
            
            // 還未拿到碎片 B 重置碎片位置。
            if (haveKey12 == false)
            {
                key12.transform.localScale = Vector3.zero;
            }

            fansToggle.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 30f));
            fans.transform.localRotation = Quaternion.Euler(new Vector3(0f, 40f, 0f));
        }

        public void FansWork()
        {
            if (isFansOpen)
                return;
            
            var tween = fansBlade.transform.DOLocalRotate(new Vector3(0f, 0f, 360f), 1.5f, RotateMode.FastBeyond360)
                .SetLoops(2, LoopType.Incremental)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    isFansOpen = false;
                });
            
            if (this.IsLeftGreenOnCurrent && this.IsRightBlueOnCurrent)
            {
                if (stage_11_rightBlue == null)
                    return;
                
                if (stage_11_rightBlue.CloudGameObject == null)
                    return;
                
                var cloud = stage_11_rightBlue.CloudGameObject;
                cloud.transform.DOMoveX(cloud.transform.position.x + 5f, 1f)
                    .OnComplete(() =>
                    {
                        cloud.transform.SetParent(this.transform);
                        cloud.transform.localPosition = new(-4f, cloud.transform.localPosition.y, cloud.transform.localPosition.z);
                        cloud.transform.DOLocalMoveX(4f, 1.5f)
                            .OnComplete(() =>
                            {
                                _cloud = cloud;
                            });
                    });
            }
            
            if (this.IsLeftGreenOnCurrent && this.IsRightGreenOnCurrent)
            {
                Debug.Log("確認是否有右綠的程式");
                if (stage_12_rightGreen == null)
                    return;

                Debug.Log("確認是否擁有白雲");
                if (_cloud == null)
                    return;
                
                Debug.Log("白雲開始往右飄動");
                var cloudMove = _cloud.transform.DOMoveX(_cloud.transform.position.x + 5f, 1f)
                    .OnComplete(() =>
                    {
                        _cloud.transform.SetParent(stage_12_rightGreen.transform);
                        stage_12_rightGreen.GotCloud(_cloud);
                    });
            }
        }

        /// <summary>
        /// 調整風向方向後出現碎片右綠 key12
        /// </summary>
        public void FindKey12()
        {
            if (isFansTurn)
                return;
            
            var toggleSequence = DOTween.Sequence();
            toggleSequence.Pause();
            
            var toggleRotate = fansToggle.transform.DOLocalRotate(new Vector3(0f, 0f, -30f), 0.5f);
            toggleRotate.Pause();
            toggleSequence.Append(toggleRotate);
            
            
            var fansRotate = fans.transform.DOLocalRotate(new Vector3(0f, -40f, 0f), 0.5f);
            fansRotate.Pause();
            toggleSequence.Append(fansRotate);
            
            isFansTurn = true;
            
            var key12Show = key12.transform.DOScale(Vector3.one, 0.01f);
            key12Show.Pause();
            toggleSequence.Append(key12Show);
            toggleSequence.Play();
        }

        public void OnKey12ObjectClick()
        {
            var maskBGotTween = key12.transform.DOMove(endPos.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveKey12 = true;
                if (gameManager != null)
                {
                    gameManager.isMaskF_active = true;
                }
            };
        }
    }
}