using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public float hp = 100f;
    public Slider hpSlider;
    
    public Canvas target; // 타겟을 Canvas 타입으로 변경
    public float fallDamageThreshold = 1f; // 낙하 데미지를 입히기 위한 속도 임계값
    public float weightDamageThreshold; // 무거운 물체에 눌릴 때의 무게 임계값

    private Rigidbody rb;

    public Camera enemyCam;
    
    public SoundManager soundManager;
    
    private bool isDead = false;

   void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        rb = GetComponent<Rigidbody>();
        weightDamageThreshold = rb.mass;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체의 속도를 이용해 데미지를 계산
        float damage = collision.relativeVelocity.magnitude;
        Debug.Log("데미지 :" + damage);
        TakeDamage(damage);
        
        // 충돌한 물체의 무게를 이용해 데미지를 계산
        Rigidbody collisionRb = collision.rigidbody;
        if (collisionRb != null && collisionRb.mass > weightDamageThreshold)
        {
            float weightDamage = (collisionRb.mass - weightDamageThreshold) * collision.relativeVelocity.magnitude;
            Debug.Log("무게 데미지 :" + weightDamage);
            TakeDamage(weightDamage);
        }
        
        if (soundManager != null && damage > 10) 
        { 
            soundManager.OnEventSound(3); 
        }
    }

    void FixedUpdate()
    {
        // 일정 속도 이상으로 낙하할 경우 데미지를 입힘
        if (rb.velocity.magnitude >= fallDamageThreshold)
        {
            float fallDamage = (rb.velocity.magnitude - fallDamageThreshold);
            TakeDamage(fallDamage);
        }
    }

    void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("HP: " + hp);
        hpSlider.value = hp; // 슬라이더의 값을 업데이트

        if (hp <= 0)
        {
            if (!isDead) // 최초로 0 이하가 된 경우
            {
                isDead = true; // 사운드가 이미 재생되었음을 기록해서 한번만 재생시키게 변경
                if (soundManager != null) 
                { 
                    soundManager.OnEventSound(4); 
                }
            }
            StartCoroutine(DestroyAfterDelay());
        }
    }


    System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void Update()
    {
        if (Camera.main != null)
        {
            hpSlider.transform.LookAt(Camera.main.transform);
        }
        
        else if (enemyCam.gameObject.activeInHierarchy)
        {
            hpSlider.transform.LookAt(enemyCam.transform);
        }
        
    }
}
