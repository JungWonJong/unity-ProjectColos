using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    //Enemy�� hp�� �����ϱ� ���� ���� ����
    [SerializeField] private Slider HpSlider;
    int MaxHp = 0;
    int NowHp = 0;

    //���� ���� ���� ����
    public Image ReactionColorImage;
    public float ReColorReactionTime = 3.0f;
    bool isColorReaction = false; //���� ���� ����
    Color NowReactionColor = Color.black;
    Color PurpleReaction = new Color(1, 0, 1, 1);
    Color YellowReaction = new Color(1, 1, 0, 1);
    Color SkyblueReaction = new Color(0, 1, 1, 1);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������ �ִ�ü�� ���� ü�� �ݿ�
        HpSlider.maxValue = MaxHp;
        HpSlider.value = NowHp;

        //�� ����
        ReactionColorImage.GetComponent<Image>().color = NowReactionColor;

        //���� ���� ��Ʈ��
        if ((NowReactionColor == PurpleReaction || NowReactionColor == YellowReaction || NowReactionColor == SkyblueReaction) && !isColorReaction)
        {
            StartCoroutine(ColorReactionAction(NowReactionColor));
        }
    }

    //������ ��Ÿ�� �� �ٽ� ��ĥ�� ���� �����
    IEnumerator ColorReactionAction(Color ReactionColor)
    {
        this.GetComponent<EnemyController>().HurtEnemy(30, 0, null);//������ �ο�
        isColorReaction = true; //���ݿ� ���� true
        yield return new WaitForSeconds(ReColorReactionTime);
        NowReactionColor = Color.black; //�� �����·�
        isColorReaction = false; //������ ���� false
    }

    //�ִ� Hp��Ʈ��
    public int GSMaxHp
    {
        get
        {
            return MaxHp;
        }
        set
        {
            MaxHp = value;
        }
    }
    //���� Hp��Ʈ��
    public int GSNowHp
    {
        get
        {
            return NowHp;
        }
        set
        {
            NowHp = value;
        }
    }

    //���� ���� ���� ��Ʈ��
    public Color GSNowReactionColor
    {
        get
        {
            return NowReactionColor;
        }
        set
        {
            if (!isColorReaction)
            {
                NowReactionColor += value;
                if (NowReactionColor.a == 2)
                {
                    NowReactionColor.a -= 1;
                }
            }
        }
    }
}
