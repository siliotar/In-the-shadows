using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject GameField;

    float Trunc(float value, float min, float max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;
        return value;
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 rotation;
        float dy = (mousePos.y - Screen.height / 2) / Screen.height;
        dy = Trunc(dy, -0.5f, 0.5f);
        rotation.x = dy * 100.0f;
        rotation.y = 0.0f;
        float dx = (Screen.width / 2 - mousePos.x) / Screen.width;
        dx = Trunc(dx, -0.5f, 0.5f);
        rotation.z = dx * 100.0f;
        Quaternion quat = new Quaternion { eulerAngles = rotation };
        GameField.GetComponent<Rigidbody>().MoveRotation(Quaternion.LerpUnclamped(GameField.transform.rotation, quat, Time.fixedDeltaTime));
    }
}
