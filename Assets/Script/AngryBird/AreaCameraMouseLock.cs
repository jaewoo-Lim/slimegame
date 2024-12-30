using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCameraMouseLock : MonoBehaviour
{
    public Camera areaCamera;
    void Update()
    {
        if (areaCamera != null && areaCamera.gameObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
