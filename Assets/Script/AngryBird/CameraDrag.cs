using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public Transform sphere; 
    private float sphereRadius;
    
    public Transform camera; 
    public float rotationSpeed = 5f; 
    public float minYPosition = 5; 
    
    private Vector3 lastMousePosition;

    void Start()
    {
        // 구체의 표면을 따라 카메라를 이동시킬 것. 구체의 반지름 계산 (Collider에서 정보 가져오기)
        SphereCollider sphereCollider = sphere.GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            sphereRadius = sphereCollider.radius * sphere.localScale.x; // 구체의 월드 크기를 반영
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            float angleX = delta.x * rotationSpeed * Time.deltaTime;
            float angleY = delta.y * rotationSpeed * Time.deltaTime;

            // 카메라 위치를 구체 중심에서의 방향 벡터로 재계산. 여기서 부터 잘 모르겠어서 AI 도움 받음.
            Vector3 direction = (camera.position - sphere.position).normalized; // 현재 구체에서 카메라로의 방향
            Quaternion rotationHorizontal = Quaternion.AngleAxis(angleX, Vector3.up); // Y축 회전
            Quaternion rotationVertical = Quaternion.AngleAxis(angleY, Vector3.Cross(Vector3.up, direction)); // 구체 표면의 수직 회전

            // 두 회전을 결합하여 새로운 방향 계산
            direction = rotationHorizontal * rotationVertical * direction;

            // 카메라 위치 업데이트: 구체의 표면을 따라 이동
            Vector3 newPosition = sphere.position + direction * sphereRadius;   // 여기까지 AI 도움 받음.

            // Y값 범위 제한
            if (newPosition.y < minYPosition)
            {
                newPosition.y = minYPosition;
            }
            
            camera.position = newPosition;
            
            camera.LookAt(sphere.position);
        }
    }
}

