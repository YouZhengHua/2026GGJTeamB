using UnityEngine;
using UnityEngine.SceneManagement;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MainMenuUI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private GameObject menuRoot;        // Canvas/MenuRoot
    [SerializeField] private GameObject creditsPanel;    // Canvas/Panel_Credits
    [SerializeField] private GameObject optionsPanel;    // Canvas/Panel_Options

    [Header("Optional")]
    [SerializeField] private string gameplaySceneName = "Main";

    void Awake()
    {
        // 預設：只開主選單
        if (menuRoot != null) menuRoot.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    void Update()
    {
        if (!IsEscPressedThisFrame()) return;

        // Esc：若在 Credits 或 Options，返回主選單
        if (creditsPanel != null && creditsPanel.activeSelf)
        {
            CloseCredits();
            return;
        }

        if (optionsPanel != null && optionsPanel.activeSelf)
        {
            CloseOptions();
            return;
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
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (menuRoot != null) menuRoot.SetActive(true);
    }

    public void OpenOptions()
    {
        if (menuRoot != null) menuRoot.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (menuRoot != null) menuRoot.SetActive(true);
    }
}
