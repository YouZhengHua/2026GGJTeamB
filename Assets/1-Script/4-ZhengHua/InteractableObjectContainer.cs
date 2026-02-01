using System;
using UnityEngine;

namespace ZhengHua
{
    public class InteractableObjectContainer : MonoBehaviour
    {
        [SerializeField] private StageManager stageManager;
        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            stageManager?.StageInit();
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