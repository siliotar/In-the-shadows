using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour
{
    public void NormalMode()
    {
        Player.mode = GameMode.Normal;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TestMode()
    {
        for (int i = 0; i < Player.Levels.Length; ++i)
            if (Player.Levels[i] == LevelInfo.Inactive)
                Player.Levels[i] = LevelInfo.Active;
        Player.mode = GameMode.Test;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
