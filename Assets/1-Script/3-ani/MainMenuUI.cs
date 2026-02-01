using UnityEngine;
using UnityEngine.SceneManagement;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MainMenuUI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private GameObject menuRoot;       // Canvas/MenuRoot
    [SerializeField] private GameObject creditsPanel;   // Canvas/Panel_Credits

    [Header("Optional")]
    [SerializeField] private string gameplaySceneName = "Main";

    void Awake()
    {
        if (menuRoot != null) menuRoot.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    void Update()
    {
        if (!IsEscPressedThisFrame()) return;

        // 只在 Credits 開啟時，Esc 回主選單
        if (creditsPanel != null && creditsPanel.activeSelf)
        {
            CloseCredits();
        }
    }

    private static bool IsEscPressedThisFrame()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
#else
        return Input.GetKeyDown(KeyCode.Escape);
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenCredits()
    {
        if (menuRoot != null) menuRoot.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (menuRoot != null) menuRoot.SetActive(true);
    }
}
