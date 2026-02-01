using System.Security;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{
    /// <summary>
    /// 右綠
    /// 樹苗(下雨後成長為松樹), 蹺蹺板, 松樹上有松果
    ///
    /// 於流程六接收白雲後，開始下雨，並使樹苗成長為松樹，接流程七。
    ///
    /// 流程七
    /// 松樹成長完畢後，於樹下出現碎片右紅 key10
    /// </summary>
    public class Stage_12_RightGreen : StageManager
    {
        private GameObject _cloud;
        
        private bool haveKey10 = false;

        [SerializeField] private GameObject key10;
        [SerializeField] private Transform endPos;

        [SerializeField] private GameObject rain;
        [SerializeField] private GameObject tree;

        [SerializeField] private GameObject leftFruit;
        private bool leftFruitClick = false;
        [SerializeField] private GameObject rightFruit;
        private bool rightFruitClick = false;

        private bool part8Done = false;
        
        private GameManager gameManager;

        [SerializeField] private Stage_02_LeftRed stage02LeftRed;
        
        private void Start()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        public override void StageInit()
        {
            base.StageInit();
            
            AudioManager.Instance.PlayBGM("forest_Music");

            if (_cloud == null)
                return;

            if (part8Done)
                return;
            
            leftFruitClick = false;
            rightFruitClick = false;
            leftFruit.transform.localPosition = new Vector3(3.82f, 1.88f, -1f);
            rightFruit.transform.localPosition = new Vector3(6.32f, 2.03f, -1f);
        }
        
        public void GotCloud(GameObject cloud)
        {
            if (haveKey10)
                return;
            
            _cloud = cloud;
            rain.transform.DOScale(Vector3.one, 0.5f)
                .OnComplete(() =>
                {
                    rain.transform.DOMoveY(-1f, 1f)
                        .OnComplete(() =>
                        {
                            rain.transform.DOScaleY(0f, 0.5f);
                            _cloud.transform.DOScaleY(0f, 0.5f);
                            tree.transform.DOScaleY(5f, 1.5f)
                                .OnComplete(() =>
                                {
                                    tree.GetComponentInChildren<Text>().text = "松\n樹";
                                    key10.transform.DOScale(Vector3.one, 0.5f);
                                    leftFruit.transform.DOScale(Vector3.one, 0.5f);
                                    rightFruit.transform.DOScale(Vector3.one, 0.5f);
                                });
                        });
                });
        }

        public void OnKey10ObjectClick()
        {
            var maskBGotTween = key10.transform.DOMove(endPos.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveKey10 = true;
                if (gameManager != null)
                {
                    gameManager.isMaskD_active = true;
                }
            };
        }

        public void OnLeftFruitClick()
        {
            if (leftFruitClick)
                return;

            leftFruit.transform.DOShakeRotation(1f, 35f, 4)
                .OnComplete(() =>
                {
                    leftFruit.transform.DOMoveY(-2.5f, 0.5f).OnComplete(() =>
                    {
                        leftFruitClick = true;

                        if (rightFruitClick)
                        {
                            rightFruit.transform.DOLocalMove(new Vector3(12f, 8f), 3f);
                        }
                    });
                });
        }

        public void OnRightFruitClick()
        {
            if (rightFruitClick)
                return;

            rightFruit.transform.DOShakeRotation(1f, 35f, 4)
                .OnComplete(() =>
                {
                    rightFruit.transform.DOMoveY(-2.5f, 0.5f).OnComplete(() =>
                    {
                        rightFruitClick = true;
                        if (leftFruitClick)
                        {
                            if (this.IsLeftRedOnCurrent && this.IsRightGreenOnCurrent)
                            {
                                leftFruit.transform.DOLocalMove(new Vector3(-4f, 0.75f), 2.2f)
                                    .OnComplete(() =>
                                    {
                                        part8Done = true;
                                        leftFruit.transform.SetParent(stage02LeftRed.transform);
                                        stage02LeftRed.SetFruit(leftFruit);
                                    });
                            }
                            else
                            {
                                leftFruit.transform.DOLocalMove(new Vector3(-12f, 8f), 3f);
                            }
                        }
                    });
                });
        }
    }
}