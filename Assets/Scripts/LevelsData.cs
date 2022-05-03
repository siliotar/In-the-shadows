using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object
{
    public string name;
    public Vector3 position;
    public bool mirrorTolerance;
    public bool freeUp;
    public float mass;
}

[System.Serializable]
public class LevelData
{
    public string name;
    public int difficulty;
    public List<Object> objects;
    public List<Vector3> additiontalCorrectRotations;
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
