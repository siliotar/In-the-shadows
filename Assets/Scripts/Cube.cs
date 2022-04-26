using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Cube : MonoBehaviour
{
    public Material Base;
    public Material Inactive;
    public Material SelectedUnsolved;
    public Material Solved;
    public Material SelectedSolved;

    public int LevelID;

    private static int _selectedLevel = -1;

    private bool _active = true;
    private Renderer _renderer;
    private TMP_Text LevelNameUI;

    private void Start()
    {
        LevelNameUI = GameObject.FindGameObjectWithTag("LevelName").GetComponent<TMP_Text>();
        _renderer = gameObject.GetComponent<Renderer>();
        if (PlayerData.Levels[LevelID] == LevelInfo.Inactive)
        {
            _active = false;
            _renderer.material = Inactive;
        }
    }

    private void Update()
    {
        if (_active && _selectedLevel != LevelID)
        {
            if (PlayerData.Levels[LevelID] == LevelInfo.Solved)
                _renderer.material = Solved;
            else
                _renderer.material = Base;
        }
    }

    private void OnMouseDown()
    {
        if (_active)
        {
            SelectedLevel._lvl = LevelID;
            if (_selectedLevel == LevelID)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
            {
                LevelNameUI.text = Levels.data[LevelID].name;
                _selectedLevel = LevelID;
                if (PlayerData.Levels[LevelID] == LevelInfo.Solved)
                    _renderer.material = SelectedSolved;
                else
                    _renderer.material = SelectedUnsolved;
            }
        }
    }
}
