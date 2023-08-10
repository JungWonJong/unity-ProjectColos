using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int TrapDamage = 10;
    public float KnockbackForce = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾� �ǰ� �Լ� ȣ��
            PlayerController.instance.Hurt(TrapDamage, KnockbackForce, transform);

            // �ش� ���� ��Ȱ��ȭ �Ǵ� ����
            gameObject.SetActive(false);
            // �Ǵ� Destroy(gameObject);
        }
    }
}