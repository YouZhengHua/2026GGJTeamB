using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZhengHua
{
    /// <summary>
    /// 左紅
    /// 太陽燈, 金字塔。
    /// 流程二
    /// 點擊太陽拉繩釣出碎片左綠 key00
    /// </summary>
    public class Stage_02_LeftRed : StageManager
    {
        private bool haveKey00 = false;

        [SerializeField] private GameObject key00;
        [SerializeField] private GameObject lightToggle;

        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        [SerializeField] private Transform gotPos1;
        [SerializeField] private Transform gotPos2;

        [SerializeField] private Stage_10_RightRed rightRed;

        private bool isLightOpened = false;

        private GameObject _fruit;

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
            if (haveKey00 == false)
            {
                key00.transform.position = startPos.position;
            }

            lightToggle.transform.localRotation = Quaternion.identity;
            
            AudioManager.Instance.PlayBGM("desert_wind_Music");
        }

        public void FindKey01()
        {
            if (isLightOpened)
                return;

            var lightToggleSequence = DOTween.Sequence();
            var lightToggleDown = lightToggle.transform.DOMoveY(lightToggle.transform.position.y - 1f, 0.3f);
            lightToggleDown.Pause();
            var lightToggleUp = lightToggle.transform.DOMoveY(lightToggle.transform.position.y, 0.2f);
            lightToggleUp.Pause();
            lightToggleSequence.Append(lightToggleDown);
            lightToggleSequence.Append(lightToggleUp);
            lightToggleSequence.Pause();
            if (haveKey00)
            {
                lightToggleSequence.Play();
                return;
            }

            isLightOpened = true;

            var key01Down = key00.transform.DOMove(gotPos1.position, 0.5f);
            key01Down.Pause();
            var key01End = key00.transform.DOMove(gotPos2.position, 1.5f);
            key01End.SetEase(Ease.Linear);
            key01End.Pause();
            var key01Rotate = key00.transform.DORotate(new Vector3(0f, 0f, -360f), 1.5f, RotateMode.FastBeyond360);
            key01Rotate.SetEase(Ease.Linear);
            key01Rotate.Pause();

            lightToggleSequence.Append(key01Down);
            lightToggleSequence.Append(key01End);
            lightToggleSequence.Insert(1, key01Rotate);
            lightToggleSequence.Play();
        }

        public void OnKey01ObjectClick()
        {
            /*
            var maskBGotTween = key00.transform.DOMove(endPos.position, 1f);
            maskBGotTween.onComplete = () =>
            {
                haveKey00 = true;
                if (gameManager != null)
                {
                    gameManager.isMaskA_active = true;
                }
            };
            */
            haveKey00 = true;
            key00.gameObject.SetActive(false);
            MaskManager.Instance.UnlockMask(MaskType.LeftGreen);
        }

        public void SetFruit(GameObject fruit) => _fruit = fruit;

        public void OnFruitClick()
        {
            if (_fruit == null)
                return;

            var shake = _fruit.transform.DOShakeRotation(1, 35f, 4);

            if (this.IsRightRedOnCurrent)
            {
                shake.OnComplete(() =>
                {
                    _fruit.transform.DOMove(gotPos2.position, 1f)
                        .OnComplete(() =>
                        {
                            _fruit.transform.DOMoveZ(-1f, 0f);
                            _fruit.transform.DOMoveX(3.86f, 1.5f)
                                .OnComplete(() =>
                                {
                                    _fruit.transform.SetParent(rightRed.transform);
                                    rightRed.GetFruit(_fruit);
                                });
                        });
                });
            }
        }

        private GameObject _fish;
        public void GotFish(GameObject fish)
        {
            _fish = fish;
        }
        private bool _isCooking = false;

        public void CookFish()
        {
            if (_fish == null || _isCooking)
                return;

            _isCooking = true;

            _fish.transform.DOShakeRotation(1f, 25f, 9)
                .OnComplete(() =>
                {
                    if (this.IsRightRedOnCurrent)
                    {
                        _fish.transform.DOMove(gotPos2.position, 1f)
                            .OnComplete(() =>
                            {
                                _fish.transform.DOMove(new Vector3(3.86f, 0.6f, -1f), 1.5f)
                                    .OnComplete(() =>
                                    {
                                        _fish.transform.SetParent(rightRed.transform);
                                        rightRed.GotFish(_fish);
                                    });
                            });
                    }
                    else
                    {
                        _isCooking = false;
                    }
                });
        }
    }
}