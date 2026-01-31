using DG.Tweening;
using UnityEngine;

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
        public GameObject CloudGameObject => _cloud;
        
        private bool haveKey10 = false;

        [SerializeField] private GameObject key10;
        [SerializeField] private Transform endPos;

        [SerializeField] private GameObject rain;
        
        private GameManager gameManager;
        
        private void Start()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
        
        public void GotCloud(GameObject cloud)
        {
            if (haveKey10)
                return;
            
            _cloud = cloud;
            Debug.Log("右綠取得雲朵 開始要下雨");
        }
        
        private void FindKey10()
        {
            Debug.Log("右綠 松樹成長完畢 出現碎片右紅 key10");
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
    }
}