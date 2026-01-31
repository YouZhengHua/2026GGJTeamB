using UnityEngine;

namespace ZhengHua
{
    public class InteractableObjectContainer : MonoBehaviour
    {
        [SerializeField] private StageManager stageManager;
        public void Show()
        {
            stageManager?.StageInit();
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (gameObject.activeSelf)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }
    }
}