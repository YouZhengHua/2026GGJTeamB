using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;       // Canvas/MenuRoot
    [SerializeField] private GameObject creditsPanel;   // Canvas/Panel_Credits
    [SerializeField] private string gameplaySceneName = "Main";

    private void Awake()
    {
        if (menuRoot != null) menuRoot.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
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

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
