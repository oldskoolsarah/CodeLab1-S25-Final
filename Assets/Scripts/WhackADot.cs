using UnityEngine;
using System;
using Random = UnityEngine.Random;
//Using JetBrains.Annotations;

public class WhackADot : MonoBehaviour
{

    public float range = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMouseDown()
    {

    //TODO Implement this method
    //throw new System.NotImplementedException();

        GameManager.instance.Score++;

        transform.position = new Vector3(
        Random.Range(-range, range),
        Random.Range(-range, range));

    }
}
