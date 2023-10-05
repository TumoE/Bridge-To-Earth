using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectsRandomizerSO:MonoBehaviour
{
    [SerializeField] private Transform[] _objects;

    public void RandomRotation()
    {
        float x,y,z;

        for (int i = 0; i < _objects.Length; i++)
        {
            x = Random.Range(-5f, 5f);
            y = Random.Range(0, 360f);
            z = Random.Range(-5f, 5f);

            _objects[i].transform.rotation = Quaternion.Euler(x,y,z);
        }
    }
    public void RandomScale()
    {
        float x, y, z;

        for (int i = 0; i < _objects.Length; i++)
        {
            x = y = z = Random.Range(.9f, 1.2f);
            y += Random.Range(0.1f, 0.3f);

            _objects[i].transform.localScale = new Vector3(x, y, z);
        }
    }
}
