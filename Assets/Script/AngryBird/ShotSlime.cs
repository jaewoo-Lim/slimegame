using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSlime : MonoBehaviour
{
    public PowerGauge powerGauge; // PowerGauge 스크립트 참조
    public GameObject prefabToLaunch; // 발사할 프리팹
    public Transform launchPoint; // 발사 지점
    public LineRenderer lineRenderer; // 궤적을 시각화할 Line Renderer
    public int lineSegmentCount = 100; // 궤적 선분 수
    public float timeStep = 0.1f; // 시뮬레이션 시간 간격
    public Camera playerCamera; // 플레이어 카메라
    public float maxTrajectoryLength = 3f; // 궤적의 최대 길이
    public GameObject sliderUI; // 슬라이더 UI
    
    public SoundManager soundManager;

    void Start()
    {
    }

    void Update()
    {
        if (playerCamera == null || !playerCamera.gameObject.activeInHierarchy) 
        {
            sliderUI.SetActive(false); 
            return;
        }
        // 스페이스 바를 떼면 오브젝트를 발사합니다
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float power = powerGauge.GetCurrentPower();
            LaunchObject(power);
            lineRenderer.enabled = false; // 궤적 비활성화
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            // 궤적 시각화
            float power = powerGauge.GetCurrentPower();
            DrawTrajectory(power);
        }
        
    }

    void LaunchObject(float power)
    {
        // 프리팹을 설정된 파워로 날림.
        GameObject launchObject = Instantiate(prefabToLaunch, launchPoint.position, launchPoint.rotation);
        Rigidbody rb = launchObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 launchDirection = playerCamera.transform.forward; // 카메라의 방향으로 발사.
            Vector3 launchForce = launchDirection * power * rb.mass;
            
            rb.AddForce(launchForce, ForceMode.Impulse);
            rb.useGravity = true;
            
            StartCoroutine(DestroyAfterTime(launchObject, 5f));
        }
        
        if (soundManager != null)
        {
            soundManager.OnEventSound(2);
            Debug.Log("사운드 재생");
        }
        
        else if (soundManager == null)
        {
            Debug.LogError("SoundManager가 할당되지 않았습니다.");
        }
        
       
        powerGauge.ResetPower(); // 파워 게이지 초기화
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    
    void DrawTrajectory(float power)
    {
        // 궤적을 리스트로 점들을 계산하여 LineRenderer로 시각화.
        lineRenderer.positionCount = lineSegmentCount; // 라인 렌더러의 점 개수 설정
        Vector3[] points = new Vector3[lineSegmentCount]; // 궤적 점 배열 초기화
        Vector3 startingPosition = launchPoint.position; // 시작 위치
        Vector3 launchDirection = playerCamera.transform.forward; // 발사 방향
        float launchPower = power * prefabToLaunch.GetComponent<Rigidbody>().mass; // 질량을 곱한 발사 힘
        Vector3 startingVelocity = launchDirection * launchPower; // 시작 속도 계산

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float time = i * timeStep; // 현재 시간 계산
            Vector3 point = startingPosition + startingVelocity * time + 0.5f * Physics.gravity * time * time; // 궤적 계산. 중력을 사용해서 중가속도 공식 사용.
            points[i] = point; // 점 배열에 추가

            // 궤적 길이 제한
            if (i > 0 && Vector3.Distance(points[i - 1], points[i]) > maxTrajectoryLength)
            {
                lineRenderer.positionCount = i + 1;
                break;
            }
        }
        lineRenderer.SetPositions(points); // 라인 렌더러에 점 설정
        lineRenderer.enabled = true; // 라인 렌더러 활성화
    }

}
