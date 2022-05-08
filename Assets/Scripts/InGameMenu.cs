using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private static GameObject NextButton = null;
    private static GameObject Menu = null;
    private static GameObject OptionsMenu = null;
    private static GameObject ControlSettings = null;
    private static GameObject Panel = null;
    private static bool _isActive;

    private void Start()
    {
        _isActive = false;
        if (NextButton == null)
        {
            NextButton = GameObject.Find("NextButton");
            NextButton.SetActive(false);
        }
        if (Menu == null)
        {
            Menu = GameObject.Find("Menu");
            Menu.SetActive(false);
        }
        if (OptionsMenu == null)
        {
            OptionsMenu = GameObject.Find("OptionsMenu");
            OptionsMenu.SetActive(false);
        }
        if (ControlSettings == null)
        {
            ControlSettings = GameObject.Find("Control Settings");
            ControlSettings.SetActive(false);
        }
        if (Panel == null)
        {
            Panel = GameObject.Find("Panel");
            Panel.SetActive(false);
        }
    }

    public static void SetActive(bool value)
    {
        if (_isActive)
        {
            Menu.SetActive(false);
            OptionsMenu.SetActive(false);
            ControlSettings.SetActive(false);
            Panel.SetActive(false);
        }
        else
        {
            if (SelectedLevel._lvl + 1 < Player.Levels.Length && Player.Levels[SelectedLevel._lvl + 1] != LevelInfo.Inactive)
                NextButton.SetActive(true);
            Menu.SetActive(true);
            Panel.SetActive(true);
        }
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
