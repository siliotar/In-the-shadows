using UnityEngine;

public class Controller : MonoBehaviour
{
    private Vector3 _prevMousePos;
    private Vector3 _objectCenter;
    private bool _pressed = false;

    public static GameObject TempObject;

    private void Start()
    {
        TempObject = GameObject.FindGameObjectWithTag("Object");
        _objectCenter.x = -3.0f;
        _objectCenter.y = 3.3f;
        _objectCenter.z = 0.0f;
    }

    private void Update()
    {
        if (TempObject == null)
            return;
        if (Input.GetMouseButton(0))
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
            if (Levels.data[SelectedLevel._lvl].difficulty == 0 ||
                !(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ||
                Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ||
                Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
                TempObject.transform.Rotate(0, mouseDiff.x * Time.deltaTime * 2000.0f, 0, Space.World);
            if (Levels.data[SelectedLevel._lvl].difficulty > 0)
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    TempObject.transform.Rotate(0, 0, -mouseDiff.y * Time.deltaTime * 2000.0f, Space.World);
                else if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                    TempObject.transform.Rotate(mouseDiff.x * Time.deltaTime * 2000.0f, 0, 0, Space.World);
                else if (Levels.data[SelectedLevel._lvl].difficulty > 1)
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
            _prevMousePos = tempMousePos;
        }
        else
            _pressed = false;
    }
}
