using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject _currentKey;

    public static string moveParameter = "MoveKey";
    public static string rollParameter = "RollKey";

    public TMP_Text move, roll;

    private Color32 _normal = new Color32(255, 255, 255, 0);
    private Color32 _highlighted = new Color32(255, 255, 255, 60);

    public void SetKey(GameObject key)
    {
        if (_currentKey != null)
            _currentKey.GetComponent<Image>().color = _normal;

        _currentKey = key;
        _currentKey.GetComponent<Image>().color = _highlighted;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(moveParameter, Convert.ToInt32(keys[moveParameter]));
        PlayerPrefs.SetInt(rollParameter, Convert.ToInt32(keys[rollParameter]));
    }

    void Start()
    {
        KeyCode moveCode = (KeyCode)PlayerPrefs.GetInt(moveParameter, Convert.ToInt32(KeyCode.LeftShift));
        KeyCode rollCode = (KeyCode)PlayerPrefs.GetInt(rollParameter, Convert.ToInt32(KeyCode.LeftAlt));
        keys.Add(moveParameter, moveCode);
        keys.Add(rollParameter, rollCode);
        move.text = moveCode.ToString();
        roll.text = rollCode.ToString();
    }

    private void OnGUI()
    {
        if (_currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey && e.keyCode != KeyCode.None && !keys.ContainsValue(e.keyCode))
            {
                keys[_currentKey.name] = e.keyCode;
                _currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();
                _currentKey.GetComponent<Image>().color = _normal;
                _currentKey = null;
            }
        }
    }
}
