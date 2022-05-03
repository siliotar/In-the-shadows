using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour
{
    private void Awake()
    {
        LevelData tempLevelData = Levels.data[SelectedLevel._lvl];
        List<Object> objs = tempLevelData.objects;
        int difficulty = tempLevelData.difficulty;
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < objs.Count; ++i)
        {
            GameObject temp = Instantiate(Resources.Load(objs[i].name)) as GameObject;
            gameObjects.Add(temp);
            Quaternion randomRotation = Random.rotation;
            for (int j = 0; j < 20; ++j)
            {
                if (difficulty == 0)
                {
                    randomRotation.x = 0;
                    randomRotation.z = 0;
                    randomRotation.Normalize();
                }
                bool correctRotation = true;
                foreach (Vector3 rotation in tempLevelData.additiontalCorrectRotations)
                {
                    float dot = Quaternion.Dot(randomRotation, Quaternion.Euler(rotation));
                    if (dot < 0.2f || (objs[i].mirrorTolerance && dot > 0.8f))
                    {
                        randomRotation = Random.rotation;
                        correctRotation = false;
                        break;
                    }
                }
                if (correctRotation)
                    break;
            }
            temp.transform.Rotate(randomRotation.eulerAngles, Space.World);
            if (difficulty > 1)
            {
                Vector3 randomPosition = Random.insideUnitSphere;
                randomPosition.x = 0;
                temp.transform.position += randomPosition;
            }
        }
        Controller.Objects = gameObjects;
    }
}
