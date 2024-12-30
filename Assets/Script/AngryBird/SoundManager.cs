using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgmSource; // 배경 음악을 위한 AudioSource
    private AudioSource walkSource; // 발걸음 소리를 위한 AudioSource
    public AudioClip[] clips; // 소리 클립 배열

    void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>(); // BGM을 위한 AudioSource 추가
        walkSource = gameObject.AddComponent<AudioSource>(); // 발걸음 소리를 위한 AudioSource 추가
    }

    void Start()
    {
        OnBGM();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
            OnWalkSound();
        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D))
            StopWalkSound();
    }

    // 배경음악 시작
    public void OnBGM()
    {
        bgmSource.clip = clips[0]; // 배경음악 클립 설정
        bgmSource.loop = true; // BGM을 무한 반복
        bgmSource.Play(); // 배경음악 재생
    }

    // 발걸음 소리 재생
    public void OnWalkSound()
    {
        if (!walkSource.isPlaying) // 발걸음 소리가 재생 중이지 않으면
        {
            walkSource.clip = clips[1]; // 발걸음 소리 클립 설정
            walkSource.loop = true; // 발걸음 소리도 반복 재생
            walkSource.Play(); // 발걸음 소리 재생
        }
    }

    // 발걸음 소리 멈춤
    public void StopWalkSound()
    {
        walkSource.Stop(); // 발걸음 소리 멈추기
    }

    // 특정 이벤트 소리 재생
    public void OnEventSound(int clipIndex)
    {
        bgmSource.PlayOneShot(clips[clipIndex]); // 특정 소리 재생
    }
}