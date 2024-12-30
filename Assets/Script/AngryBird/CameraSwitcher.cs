using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject mainCameraObject; // 카메라 오브젝트
    public GameObject areaCameraObject; // 카메라 오브젝트

    void Start()
    {
        mainCameraObject.SetActive(true);
        areaCameraObject.SetActive(false);
    }

    void Update()
    {
    }

    public void SwitchCamera()
    {
        EventSystem.current.SetSelectedGameObject(null);    // UI버튼을 누르면 버튼이 선택되는데 그것을 풀어줌. 
        bool isMainCameraActive = mainCameraObject.activeInHierarchy;
        mainCameraObject.SetActive(!isMainCameraActive);
        areaCameraObject.SetActive(isMainCameraActive);
    }
}

