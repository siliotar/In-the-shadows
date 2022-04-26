public enum LevelInfo : int
{
    Inactive = 0,
    Active = 1,
    Solved = 2
}

public enum GameMode : int
{
    None = 0,
    Normal = 1,
    Test = 2
}

public class PlayerData
{
    public static float MusicVolume = 0.5f;
    public static LevelInfo[] Levels;
    public static GameMode mode;
}
