using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour
{
    public void NormalMode()
    {
        PlayerData.mode = GameMode.Normal;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TestMode()
    {
        for (int i = 0; i < PlayerData.Levels.Length; ++i)
            if (PlayerData.Levels[i] == LevelInfo.Inactive)
                PlayerData.Levels[i] = LevelInfo.Active;
        PlayerData.mode = GameMode.Test;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
