using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    public const string moveParameter = "MoveKey";
    public const string rollParameter = "RollKey";
    public const string sensitivityParameter = "MouseSensitivity";

    public static Dictionary<string, KeyCode> keys;

    public static float mouseSensitivity;
}
