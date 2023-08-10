using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    public float detectionRange = 4f;  // �÷��̾ �����ϴ� ����
    public float movementSpeed = 2f;   // �̵� �ӵ�

    private Transform player;           // �÷��̾��� Transform ������Ʈ
    private Rigidbody2D rb;            // Rigidbody2D ������Ʈ
    private float MoveArrow = 1;

    //�ٴ� üũ ����
    public GameObject GroundSensor;
    public GameObject PlatformSensor;
    bool OnGround = false;

    //���� üũ ���� 
    public GameObject FrontGroundSensor;
    public GameObject FrontPlatformSensor;
    bool OnFrontGround = false;

    //

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // �÷��̾� ����
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ���� üũ
        OnFrontGround = FrontGroundSensor.GetComponent<Sensor>().colSencorState() || FrontPlatformSensor.GetComponent<Sensor>().colSencorState();
        Debug.Log(OnFrontGround);
        if (distanceToPlayer < detectionRange)
        {
            // �÷��̾ �������� MoveArrow ����
            Vector2 direction = (player.position - transform.position).normalized;
            if (direction.x < 0)
                MoveArrow = -1;
            else
                MoveArrow = 1;
        }

        // 
        if (OnFrontGround)
        {
            rb.velocity = new Vector2(movementSpeed * MoveArrow, rb.velocity.y);
            Debug.Log("MoveValue: " + movementSpeed + ", " + MoveArrow);
        }
        else
        {
            MoveArrow *= -1;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //MoveArrow�� ���� ���ý����� ����
        if (MoveArrow > 0)
            this.transform.localScale = new Vector2(1, 1);
        else if (MoveArrow < 0)
            this.transform.localScale = new Vector2(-1, 1);
    }
}
