using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DirectionVectors
{
    public Vector3 up;
    public Vector3 forward;
}

[System.Serializable]
public class Object
{
    public string name;
    public List<DirectionVectors> correctDirections;
}

[System.Serializable]
public class LevelData
{
    public string name;
    public int difficulty;
    public List<Object> objects;
}

[System.Serializable]
public class LevelsData
{
    public List<LevelData> levels;
}

public class Levels
{
    public static List<LevelData> data;
}
