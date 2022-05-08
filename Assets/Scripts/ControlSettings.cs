using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
    private GameObject _currentKey;

    public TMP_Text move, roll;
    public Slider SensitivitySlider;

    private Color32 _normal = new Color32(255, 255, 255, 0);
    private Color32 _highlighted = new Color32(255, 255, 255, 60);

    public void SetKey(GameObject key)
    {
        if (_currentKey != null)
            _currentKey.GetComponent<Image>().color = _normal;

        _currentKey = key;
        _currentKey.GetComponent<Image>().color = _highlighted;
    }

    public void SetSensitivity(float value)
    {
        Controls.mouseSensitivity = value;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(Controls.moveParameter, Convert.ToInt32(Controls.keys[Controls.moveParameter]));
        PlayerPrefs.SetInt(Controls.rollParameter, Convert.ToInt32(Controls.keys[Controls.rollParameter]));
        PlayerPrefs.SetFloat(Controls.sensitivityParameter, Controls.mouseSensitivity);
        if (_currentKey != null)
            _currentKey.GetComponent<Image>().color = _normal;
    }

    void Start()
    {
        move.text = Controls.keys[Controls.moveParameter].ToString();
        roll.text = Controls.keys[Controls.rollParameter].ToString();
        SensitivitySlider.value = Controls.mouseSensitivity;
    }

    private void OnGUI()
    {
        if (_currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey && e.keyCode != KeyCode.None && !Controls.keys.ContainsValue(e.keyCode))
            {
                Controls.keys[_currentKey.name] = e.keyCode;
                _currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();
                _currentKey.GetComponent<Image>().color = _normal;
                _currentKey = null;
            }
        }
    }
}
