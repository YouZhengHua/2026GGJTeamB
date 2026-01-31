using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ZhengHua
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private InputActionReference pause;

        [SerializeField] private string menuName = "New Scene";
        
        public void BackToMenu()
        {
            SceneManager.LoadScene(menuName);
        }

        public void BackToGame()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        
        public void ShowPauseMenu()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        
        private void TogglePause()
        {
            if (canvasGroup.alpha == 0f)
            {
                ShowPauseMenu();
            }
            else
            {
                BackToGame();
            }
        }

        private void Start()
        {
            pause.action.Enable();
            pause.action.performed += _ => TogglePause();
        }
    }
}