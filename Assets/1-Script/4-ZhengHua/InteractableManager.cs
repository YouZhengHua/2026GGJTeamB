using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ZhengHua
{
    public class InteractableManager : MonoBehaviour
    {
        private void Update()
        {
            // 檢查是否點擊左鍵
            if (!Mouse.current.leftButton.wasPressedThisFrame)
                return;
            
            // 檢查是否點擊到 UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            // 射線檢查是否有碰觸到可互動物件
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.value), out var hit))
            {
                if (hit.transform.TryGetComponent<InteractableObject>(out var interactableObject))
                {
                    interactableObject.OnClick();
                }
            }
        }
    }
}