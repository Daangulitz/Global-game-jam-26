using System;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    private Transform transform;
    public float speed = 5.0f;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        var vector3 = transform.position;
        vector3.x = vector3.x + speed * Time.deltaTime;
        transform.position = vector3; 
    }
}
