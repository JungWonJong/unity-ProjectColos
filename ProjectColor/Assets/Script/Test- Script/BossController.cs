using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject rainPrefab;   // 비 프리팹
    public GameObject trapPrefab;   // 함정 프리팹

    public Transform[] trapSpawnPoints; // 함정 생성 위치 배열
    public Transform[] rainSpawnPoints; // 비 생성 위치 배열

    public float trapSpawnInterval = 5.0f; // 함정 생성 간격
    public float rainSpawnInterval = 2f; // 비 생성 간격

    public float trapDuration = 3.0f; // 함정 지속 시간
    public float rainDuration = 2.0f; // 비 지속 시간

    private float trapTimer; // 함정 생성 타이머
    private float rainTimer; // 비 생성 타이머

    private void Start()
    {
        trapTimer = trapSpawnInterval;
        rainTimer = rainSpawnInterval;
    }

    private void Update()
    {
        // 함정 생성 타이머 감소
        trapTimer -= Time.deltaTime;
        // 비 생성 타이머 감소
        rainTimer -= Time.deltaTime;

        // 일정 간격마다 함정 생성
        if (trapTimer <= 0)
        {
            SpawnTrap();
            trapTimer = trapSpawnInterval;
        }

        // 일정 간격마다 비 생성
        if (rainTimer <= 0)
        {
            SpawnRain();
            rainTimer = rainSpawnInterval;
        }
    }

    // 함정 생성 메서드
    void SpawnTrap()
    {
        // 랜덤한 위치에서 함정 생성
        int randomIndex = Random.Range(0, trapSpawnPoints.Length);
        GameObject trap = Instantiate(trapPrefab, trapSpawnPoints[randomIndex].position, Quaternion.identity);
        Destroy(trap, trapDuration); // 지정된 지속 시간 후에 함정 삭제
    }

    // 비 생성 메서드
    void SpawnRain()
    {
        // 랜덤한 위치에서 비 생성
        int randomIndex = Random.Range(0, rainSpawnPoints.Length);
        GameObject rain = Instantiate(rainPrefab, rainSpawnPoints[randomIndex].position, Quaternion.identity);
        Destroy(rain, rainDuration); // 지정된 지속 시간 후에 비 오브젝트 삭제
    }
}
