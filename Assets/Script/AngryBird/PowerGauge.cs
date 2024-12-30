using UnityEngine;
using UnityEngine.UI;

public class PowerGauge : MonoBehaviour
{
    public float maxPower = 100f;
    public float powerIncrementSpeed = 50f;
    private float currentPower;
    private bool isPowerIncreasing = true;

    public Slider powerSlider; 

    void Start()
    {
        // 슬라이더 초기 설정
        if (powerSlider != null)
        {
            powerSlider.minValue = 0;
            powerSlider.maxValue = maxPower;
            powerSlider.value = 0;
        }
    }

    void Update()
    {
        // 스페이스 바를 누르면 파워 게이지를 조절
        if (Input.GetKey(KeyCode.Space))
        {
            if (isPowerIncreasing)
            {
                currentPower += powerIncrementSpeed * Time.deltaTime;
                if (currentPower >= maxPower)
                {
                    currentPower = maxPower;
                    isPowerIncreasing = false;
                }
            }
            else
            {
                currentPower -= powerIncrementSpeed * Time.deltaTime;
                if (currentPower <= 0)
                {
                    currentPower = 0;
                    isPowerIncreasing = true;
                }
            }

            // 슬라이더 값 업데이트
            if (powerSlider != null)
            {
                powerSlider.value = currentPower;
            }
        }
    }

    public float GetCurrentPower()
    {
        return currentPower;
    }

    public void ResetPower()
    {
        currentPower = 0;
        isPowerIncreasing = true;
        if (powerSlider != null)
        {
            powerSlider.value = 0;
        }
    }
}