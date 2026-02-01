using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{
    /// <summary>
    /// 右藍
    /// 海面, 冰山, 漁船
    /// 
    /// 流程一
    /// 點擊於魚竿釣出碎片左紅 key02
    ///
    /// 流程四
    /// 檢查是否為左紅右藍，且左紅有打開太陽燈。
    /// 右藍轉為白天，並出現白雲。
    /// </summary>
    public class Stage_11_RightBlue : StageManager
    {
        /// <summary>
        /// 是否已經取得碎片左紅 key02
        /// </summary>
        private bool haveKey02 = false;
        /// <summary>
        /// 是否已經取得碎片左藍 key01
        /// </summary>
        private bool haveKey01 = false;

        #region 流程一 相關參數
        [SerializeField] private GameObject key02;
        [SerializeField] private GameObject fishingRod;

        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        [SerializeField] private Transform gotPos;
        
        private bool isFishing = false;
        private bool isFire = false;
        #endregion

        #region 流程四相關參數
        [SerializeField] private GameObject cloud;
        [SerializeField] private GameObject background;
        
        private bool isCloudShow = false;

        public GameObject CloudGameObject => isCloudShow ? cloud : null;

        #endregion

        private GameManager gameManager;
        
        [SerializeField] private Stage_00_LeftGreen leftGreen;
        [SerializeField] private GameObject iceMountain;        
        [SerializeField] private Transform endPos2;
        [SerializeField] private GameObject key01;

        [SerializeField] private GameObject fish;
        [SerializeField] private Stage_02_LeftRed leftRed;
        private bool fishEnd = false;

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

            if (!isCloudShow)
            {
                bool isNeedShowCloud = this.IsLeftRedOnCurrent && this.IsRightBlueOnCurrent;
                if (isNeedShowCloud)
                {
                    background.GetComponent<SpriteRenderer>().DOColor(Color.beige, 1f)
                        .OnComplete(() =>
                        {
                            background.GetComponentInChildren<Text>().text = "白天";
                            cloud.transform.DOScale(Vector3.one, 1.5f)
                                .OnComplete(() =>
                                {
                                    isCloudShow = true;
                                });
                        });
                }
            }

            if (haveKey01 == false && this.IsLeftGreenOnCurrent && this.IsRightBlueOnCurrent && leftGreen.HaveFireMountain)
            {
                iceMountain.transform.DOShakeScale(3f, 0.3f, 30)
                    .OnPlay(() =>
                    {
                        iceMountain.transform.DOShakeRotation(3f, 15f, 40);
                    })
                    .OnComplete(() =>
                    {
                        iceMountain.transform.DOScaleY(0.3f, 3f);
                    });
            }
            
            AudioManager.Instance.PlayBGM("Ocean_Wave_Music");
        }

        /// <summary>
        /// 顯示釣魚動作並取得碎片左紅 key02
        /// </summary>
        public void FindKey02()
        {
            // 只顯示一次釣魚動畫, 擁有 key01 時，使用另一個釣魚事件
            if (isFishing || haveKey01)
                return;
            
            var fishingRodShake = fishingRod.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0), 20, 90f);
            fishingRodShake.Pause();
            // 已經得到碎片，只顯示魚竿抖動，並且抖動完畢後可以繼續互動。
            if (haveKey02)
            {
                fishingRodShake.onComplete += () =>
                {
                    isFishing = false;
                };
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

        public void OnKey02ObjectClick()
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
        
        public void OnKey01ObjectClick()
        {
            var maskBGotTween = key01.transform.DOMove(endPos2.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveKey01 = true;
                if (gameManager != null)
                {
                    gameManager.isMaskB_active = true;
                }
            };
        }

        public void GotFish()
        {
            if (haveKey01)
            {
                isFishing = true;
                fishingRod.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0), 20)
                    .OnComplete(() =>
                    {
                        if (this.IsLeftRedOnCurrent && fishEnd == false)
                        {
                            fishingRod.transform.DOLocalRotate(new Vector3(0f, 0f, -30f), 0.3f);
                            fish.transform.DOMove(gotPos.position, 1f)
                                .OnComplete(() =>
                                {
                                    fish.transform.DOMove(new Vector3(-4f, 0.75f), 2.2f)
                                        .OnComplete(() =>
                                        {
                                            isFishing = false;
                                            fishEnd = true;
                                            fish.transform.SetParent(leftRed.transform);
                                            leftRed.GotFish(fish);
                                        });
                                });
                        }
                        else
                        {
                            isFishing = false;
                        }
                    });
            }
        }
    }
}