using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private static GameObject Menu = null;
    private static GameObject EndMenu = null;

    private static bool _isActive;

    private void Start()
    {
        _isActive = false;
        if (Menu == null)
        {
            Menu = GameObject.Find("Menu");
            Menu.SetActive(false);
        }
        if (EndMenu == null)
        {
            EndMenu = GameObject.Find("EndMenu");
            EndMenu.SetActive(false);
        }
    }

    public static void SetActive(bool value)
    {
        if (SelectedLevel._lvl + 1 < Player.Levels.Length && Player.Levels[SelectedLevel._lvl + 1] != LevelInfo.Inactive)
            EndMenu.SetActive(value);
        else
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
