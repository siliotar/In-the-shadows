using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject ControlSettings;
    public GameObject Panel;

    private bool _shown = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_shown)
            {
                OptionsMenu.SetActive(false);
                ControlSettings.SetActive(false);
                Panel.SetActive(false);
                _shown = false;
            }
            else
            {
                OptionsMenu.SetActive(true);
                Panel.SetActive(true);
                _shown = true;
            }
        }
    }

    public void Hide()
    {
        _shown = false;
    }
}
