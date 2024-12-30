using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Player 태그를 가진 오브젝트와의 충돌만 처리하고, Projectile 태그를 가진 오브젝트와의 충돌 무시
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("플레이어 충돌 확인");
        }
    }
}