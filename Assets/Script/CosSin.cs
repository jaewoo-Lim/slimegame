using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosSin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        degree += Speed * Time.deltaTime;
    }

    private float degree;
    public float radius;
    public float Speed;
    
    private void OnDrawGizmos()
    {
        Vector3 point1 = new Vector3(
            Mathf.Cos(degree * Mathf.Deg2Rad) * radius, Mathf.Sin(degree * Mathf.Deg2Rad) * radius , 0);
        Vector3 point2 = new Vector3(0,
            Mathf.Sin(degree * Mathf.Deg2Rad) * radius, 0);
        Gizmos.DrawLine(transform.position, point1);
        Gizmos.DrawLine(transform.position, point2);
        Gizmos.DrawLine(point1, point2);
    }
}