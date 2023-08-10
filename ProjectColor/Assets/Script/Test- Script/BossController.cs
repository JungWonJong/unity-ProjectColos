using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject rainPrefab;   // �� ������
    public GameObject trapPrefab;   // ���� ������

    public Transform[] trapSpawnPoints; // ���� ���� ��ġ �迭
    public Transform[] rainSpawnPoints; // �� ���� ��ġ �迭

    public float trapSpawnInterval = 5.0f; // ���� ���� ����
    public float rainSpawnInterval = 2f; // �� ���� ����

    public float trapDuration = 3.0f; // ���� ���� �ð�
    public float rainDuration = 2.0f; // �� ���� �ð�

    private float trapTimer; // ���� ���� Ÿ�̸�
    private float rainTimer; // �� ���� Ÿ�̸�

    private void Start()
    {
        trapTimer = trapSpawnInterval;
        rainTimer = rainSpawnInterval;
    }

    private void Update()
    {
        // ���� ���� Ÿ�̸� ����
        trapTimer -= Time.deltaTime;
        // �� ���� Ÿ�̸� ����
        rainTimer -= Time.deltaTime;

        // ���� ���ݸ��� ���� ����
        if (trapTimer <= 0)
        {
            SpawnTrap();
            trapTimer = trapSpawnInterval;
        }

        // ���� ���ݸ��� �� ����
        if (rainTimer <= 0)
        {
            SpawnRain();
            rainTimer = rainSpawnInterval;
        }
    }

    // ���� ���� �޼���
    void SpawnTrap()
    {
        // ������ ��ġ���� ���� ����
        int randomIndex = Random.Range(0, trapSpawnPoints.Length);
        GameObject trap = Instantiate(trapPrefab, trapSpawnPoints[randomIndex].position, Quaternion.identity);
        Destroy(trap, trapDuration); // ������ ���� �ð� �Ŀ� ���� ����
    }

    // �� ���� �޼���
    void SpawnRain()
    {
        // ������ ��ġ���� �� ����
        int randomIndex = Random.Range(0, rainSpawnPoints.Length);
        GameObject rain = Instantiate(rainPrefab, rainSpawnPoints[randomIndex].position, Quaternion.identity);
        Destroy(rain, rainDuration); // ������ ���� �ð� �Ŀ� �� ������Ʈ ����
    }
}
