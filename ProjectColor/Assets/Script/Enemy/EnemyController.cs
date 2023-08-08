using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D RBody;
    //�� ü�°��� ���� ����
    private EnemyManager EManager;
    public int MaxHealth = 100;

    //�� �ǰ� ���°��� ���� ����
    bool isHurt = false;

    //�� ������ ���� ���� ����
    public int EnemyDamage = 6;
    public int EnemyAttackDamage = 10;
    public float EnemyKnockBackPower = 5;

    public GameObject GroundSencor;
    public GameObject PlatformSencor;

    bool OnGround = false;
    bool OnPlatform = false;

    // Start is called before the first frame update
    void Start()
    {
        //�ش� ��ü�� ������ٵ� �� �ʱ�ȭ
        RBody = this.GetComponent<Rigidbody2D>();
        //EnemyManager ����
        EManager = GetComponent<EnemyManager>();
        //�ʱ� Enemyü�� �� ����
        EManager.GSMaxHp = MaxHealth;
        EManager.GSNowHp = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //�ǽð� ���� ���� ������Ʈ
        OnGround = GroundSencor.GetComponent<PlayerSensor>().colSencorState();
        OnPlatform = PlatformSencor.GetComponent<PlayerSensor>().colSencorState();

        //ĳ���Ͱ� ��Ʈ���°� �ƴ� �� �ȹи��� �ϴ� �ӽ� �ڵ�
        if (!isHurt && (OnGround || OnPlatform))
        {
            RBody.velocity = new Vector2(0,0);
        }
    }

    //�ǰ��Լ�
    public void HurtEnemy(int Damage, float KbPower, Transform ohterT)
    {
        isHurt = true;//�ǰݻ��� ����
        EManager.GSNowHp = EManager.GSNowHp - Damage;//�ǰ� ������ �ݿ�
        
        if(ohterT != null)
        {
            float x = transform.position.x - ohterT.position.x;

            x = x < 0 ? -1 : 1;

            RBody.velocity = new Vector2(0, 0);

            Vector2 KnockBackV = new Vector2(x, 1);
            RBody.AddForce(KnockBackV * KbPower, ForceMode2D.Impulse);

            StartCoroutine(KnockBackTimer());
        }
        else
        {
            if (EManager.GSNowHp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //��Ʈ �� �˹� ����
    IEnumerator KnockBackTimer()
    {
        float isTime = 0f;

        while (isTime < 0.3f)
        {
            isTime += Time.deltaTime;

            yield return null;
        }

        //�˹����� ����
        if (EManager.GSNowHp <= 0)
        {
            Destroy(this.gameObject);
        }

        isHurt = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //�÷��̾�� �浹�� ������
        if (other.gameObject.tag == "Player")
        {
            Transform thisE = gameObject.GetComponent<Transform>();//Enemy�� ��ġ��
            PlayerController.instance.Hurt(EnemyDamage, EnemyKnockBackPower, thisE);//�÷��̾� �ǰ� �Լ� ȣ��
        }
    }
}