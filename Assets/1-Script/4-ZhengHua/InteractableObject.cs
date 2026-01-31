using UnityEngine;
using UnityEngine.Events;

namespace ZhengHua
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private UnityEvent onClickEvent;

        [SerializeField] private bool needAutoHide = false;

        public void OnClick()
        {
            onClickEvent?.Invoke();

            if (needAutoHide)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}