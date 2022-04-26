using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour
{
    void Start()
    {
        List<Object> objs = Levels.data[SelectedLevel._lvl].objects;
        int difficulty = Levels.data[SelectedLevel._lvl].difficulty;
        for (int i = 0; i < objs.Count; ++i)
        {
            GameObject temp = Instantiate(Resources.Load(objs[i].name)) as GameObject;
            Quaternion randomRotation = Random.rotation;
            if (difficulty == 0)
            {
                randomRotation.x = 0;
                randomRotation.z = 0;
            }
            temp.transform.Rotate(randomRotation.eulerAngles, Space.World);
            if (difficulty > 1)
            {
                Vector3 randomPosition = Random.insideUnitSphere;
                randomPosition.x = 0;
                temp.transform.position += randomPosition;
            }
        }
    }
}
