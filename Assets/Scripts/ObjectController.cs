using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private Vector3 _mousePressPos;

    private void Start()
    {
        // Debug.Log("Up: " + gameObject.transform.up);
        // Debug.Log("Forward: " + gameObject.transform.forward);
    }

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
