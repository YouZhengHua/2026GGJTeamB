using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Main Menu Containers")]
    [SerializeField] private GameObject menuRoot;      // Canvas/MenuRoot
    [SerializeField] private GameObject creditsPanel;  // Canvas/Panel_Credits

    [Header("Scene Loading")]
    [SerializeField] private string gameplaySceneName = "Main"; // 你的遊戲場景名稱

    private void Awake()
    {
        if (menuRoot != null) menuRoot.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void StartGame()
    {
        // 點擊音效（如果你有 AudioManager）
        if (AudioManager.I != null) AudioManager.I.PlayClick();

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenCredits()
    {
        if (AudioManager.I != null) AudioManager.I.PlayClick();

        if (menuRoot != null) menuRoot.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (AudioManager.I != null) AudioManager.I.PlayClick();

        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (menuRoot != null) menuRoot.SetActive(true);
    }

    public void QuitGame()
    {
        if (AudioManager.I != null) AudioManager.I.PlayClick();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
