using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{
    /// <summary>
    /// 右紅
    /// 營火
    ///
    /// 流程九
    /// 因為松果而導致螢火爆燃
    /// </summary>
    public class Stage_10_RightRed : StageManager
    {
        [SerializeField] private GameObject fire;
        [SerializeField] private Stage_00_LeftGreen leftGreen;
        
        private bool gotFruit = false;
        private bool isBigFire = false;
        private bool isShaked = false;

        public override void StageInit()
        {
            base.StageInit();

            if (isBigFire)
            {
                AudioManager.Instance.PlayBGM("campfire_small_Music");
            }
            else
            {
                AudioManager.Instance.PlayBGM("campfire_Big_Music");
            }
        }

        public void GetFruit(GameObject fruit)
        {
            gotFruit = true;

            fire.transform.DOShakeScale(1.5f, 0.5f, 6)
                .OnPlay(() =>
                {
                    fruit.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f)
                        .OnComplete(() => fruit.transform.DOShakeScale(1f, 0.2f, 6));
                })
                .OnComplete(() =>
                {
                    fruit.transform.DOScale(Vector3.zero, 0.3f);
                    fire.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetEase(Ease.OutBack)
                        .OnComplete(() => isBigFire = true);
                });
        }

        public void ShakeFire()
        {
            if (isShaked)
                return;
            isShaked = true;
            fire.transform.DOShakeScale(1.5f, 0.5f, 6);
            fire.transform.DOShakeRotation(1.5f, 35f, 6)
                .OnComplete(() => isShaked = false);

            if (this.IsLeftGreenOnCurrent && this.IsRightRedOnCurrent && leftGreen.IsFansWorking && isBigFire)
            {
                leftGreen.GotFire();
            }
        }

        private GameObject _fish;
        public void GotFish(GameObject fish)
        {
            _fish = fish;
            _fish.GetComponent<SpriteRenderer>().DOColor(Color.darkGoldenRod, 1.5f)
                .OnComplete(() =>
                {
                    _fish.GetComponentInChildren<Text>().text = "烤魚";
                });
        }
        
        public bool HaveCookedFish => _fish != null;
    }
}