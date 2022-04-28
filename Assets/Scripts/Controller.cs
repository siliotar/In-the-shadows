using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private Vector3 _prevMousePos;
    private Vector3 _objectCenter;
    private bool _pressed = false;
    private LevelData _tempLevel;
    private bool _solved = false;
    private Vector3 _barycenter;
    private DirectionVectors[] _correctRotations;

    public static GameObject TempObject;
    public static List<GameObject> Objects;

    private void Start()
    {
        TempObject = GameObject.FindGameObjectWithTag("Object");
        _objectCenter.x = -3.0f;
        _objectCenter.y = 3.3f;
        _objectCenter.z = 0.0f;
        _tempLevel = Levels.data[SelectedLevel._lvl];
        _correctRotations = new DirectionVectors[Objects.Count];
    }

    private Vector3 CalculateBarycenter()
    {
        Vector3 result = Vector3.zero;
        foreach (GameObject tempObject in Objects)
            result += tempObject.transform.position;
        result /= Objects.Count;
        return result;
    }

    private void CheckSolve()
    {
        for (int i = 0; i < _tempLevel.objects.Count; ++i)
        {
            Object tempObjectData = _tempLevel.objects[i];
            GameObject tempGameObject = Objects[i];
            bool isCorrect = false;
            foreach (DirectionVectors vectors in tempObjectData.correctDirections)
            {
                // Check correct angle
                if ((vectors.forward == Vector3.zero || Vector3.Angle(vectors.forward, tempGameObject.transform.forward) <= 8.0f) &&
                    (vectors.up == Vector3.zero || Vector3.Angle(vectors.up, tempGameObject.transform.up) <= 8.0f))
                {
                    // Check correct position
                    if (i == 0 || ((tempGameObject.transform.position - Objects[i - 1].transform.position).sqrMagnitude < 0.01))
                    {
                        isCorrect = true;
                        _correctRotations[i] = vectors;
                        break;
                    }
                }
            }
            if (!isCorrect)
                break;
            else if (i + 1 == _tempLevel.objects.Count)
            {
                Player.Levels[SelectedLevel._lvl] = LevelInfo.Solved;
                if (SelectedLevel._lvl + 1 < Player.Levels.Length && Player.Levels[SelectedLevel._lvl + 1] == LevelInfo.Inactive)
                    Player.Levels[SelectedLevel._lvl + 1] = LevelInfo.Active;
                SaveSystem.SavePlayer();
                _solved = true;
                _barycenter = CalculateBarycenter();
                StartCoroutine(BackToMenu());
            }
        }
    }

    private void ProcessInput()
    {
        if (!InGameMenu.Active() && Input.GetMouseButton(0))
        {
            if (!_pressed)
            {
                _pressed = true;
                _prevMousePos = Input.mousePosition;
                return;
            }
            Vector3 tempMousePos = Input.mousePosition;
            Vector3 mouseDiff = _prevMousePos - tempMousePos;
            mouseDiff.x /= Screen.width;
            mouseDiff.y /= Screen.height;
            if (_tempLevel.difficulty == 0 ||
                !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ||
                Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
                TempObject.transform.Rotate(0, mouseDiff.x * Time.deltaTime * 2000.0f, 0, Space.World);
            if (_tempLevel.difficulty > 0)
            {
                if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ||
                    Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
                    TempObject.transform.Rotate(0, 0, -mouseDiff.y * Time.deltaTime * 2000.0f, Space.World);
                else if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                    TempObject.transform.Rotate(mouseDiff.x * Time.deltaTime * 2000.0f, 0, 0, Space.World);
                else if (_tempLevel.difficulty > 1)
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        Vector3 translation;
                        translation.x = 0.0f;
                        translation.y = -mouseDiff.y * Time.deltaTime * 16.0f;
                        translation.z = -mouseDiff.x * Time.deltaTime * 16.0f;
                        TempObject.transform.Translate(translation, Space.World);
                        Vector3 diff = TempObject.transform.position - _objectCenter;
                        if (diff.sqrMagnitude > 1.0f)
                        {
                            diff.Normalize();
                            TempObject.transform.position = _objectCenter + diff;
                        }
                    }
                }
            }
            // Check that the level is completed
            CheckSolve();
            _prevMousePos = tempMousePos;
        }
        else
            _pressed = false;
        if (Input.GetKeyUp(KeyCode.Escape))
            InGameMenu.SetActive(!InGameMenu.Active());
    }

    private void Update()
    {
        if (TempObject == null)
            return;
        if (!_solved)
            ProcessInput();
        else
        {
            int i = 0;
            foreach (GameObject tempObject in Objects)
            {
                // Correct position
                tempObject.transform.position = Vector3.MoveTowards(tempObject.transform.position, _barycenter, 0.1f * Time.deltaTime);
                // Correct rotation
                Vector3 up, forward;
                up = tempObject.transform.up;
                forward = tempObject.transform.forward;
                if (_correctRotations[i].up != Vector3.zero)
                    up = Vector3.MoveTowards(tempObject.transform.up, _correctRotations[i].up, 0.1f * Time.deltaTime);
                if (_correctRotations[i].forward != Vector3.zero)
                    forward = Vector3.MoveTowards(tempObject.transform.forward, _correctRotations[i].forward, 0.1f * Time.deltaTime);
                tempObject.transform.rotation = Quaternion.LookRotation(forward, up);
                ++i;
            }
        }
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2);
        InGameMenu.SetActive(true);
    }
}
