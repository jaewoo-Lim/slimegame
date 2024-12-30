using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public Vector3 direction;
    public float Speed; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 normal = Vector3.Cross(other.transform.forward, transform.right);
        float dot = Vector3.Dot(normal, -direction);
        Vector3 p = normal * dot;
        direction = direction + p;
        
        Debug.Log("OnTriggerEnter");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * (Speed * Time.deltaTime);
    }
}