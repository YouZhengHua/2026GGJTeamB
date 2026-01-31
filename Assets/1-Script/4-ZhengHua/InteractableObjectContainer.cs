using UnityEngine;

namespace ZhengHua
{
    public class InteractableObjectContainer : MonoBehaviour
    {
        public void Show()
        {
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