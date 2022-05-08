using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private Vector3 _mousePressPos;

    private void OnMouseDown()
    {
        _mousePressPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (Input.mousePosition == _mousePressPos)
            Controller.TempObject = gameObject.transform.parent.gameObject;
    }
}
