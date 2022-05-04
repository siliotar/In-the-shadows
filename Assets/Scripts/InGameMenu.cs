using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject NextButton;
    private static GameObject Menu = null;
    private static bool _isActive;

    private void Start()
    {
        _isActive = false;
        if (SelectedLevel._lvl + 1 < Player.Levels.Length && Player.Levels[SelectedLevel._lvl + 1] != LevelInfo.Inactive)
            NextButton.SetActive(true);
        if (Menu == null)
        {
            Menu = GameObject.Find("Menu");
            Menu.SetActive(false);
        }
    }

    public static void SetActive(bool value)
    {
        Menu.SetActive(value);
        _isActive = value;
    }

    public static bool Active()
    {
        return _isActive;
    }

    public void Next()
    {
        ++SelectedLevel._lvl;
        Retry();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
