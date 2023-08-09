using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //��Ŭ�� ����ȭ
    public static GameManager instance = null;
    // ���� ���� ����
    public Image nowColor;
    public Slider HealthBar;
    public Slider StaminaGage;

    bool inputNcolor;

    int NcolorCode = 3;// �������
    public Color NColor;
    
    //ü�¹�
    public int MaxHealth = 100;
    public int NowHealth;

    // ���¹̳� ����
    public int MaxStamina = 100;
    public int NowStamina;
    float staminaTimer;
    public float recoveryCycle = 0.2f; //���¹̳� ȸ���ֱ�
    public int recoveryAmount = 3; //ȸ����

    //���̵� �� �ƿ� ��ũ��
    public GameObject PadeScreen;
    public float PadeInTime = 1.5f;
    public float PadeOutTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        //
        PadeOutScreen();
        //���� ü��, ���¹̳� �ִ�ü��, ���¹̳��� ����
        NowHealth = MaxHealth;
        NowStamina = MaxStamina;
    }

    //��Ŭ�� ���� �ʱ�ȭ
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        inputNcolor = Input.GetKeyDown(KeyCode.C);
        
        if (inputNcolor)
        {
            NcolorCode += 3;

            if (NcolorCode > 10)
            {
                NcolorCode -= 9;
            }
        }

        //Ncolor�� ���� �� ����
        switch (NcolorCode)
        {
            case 3:
                NColor = Color.red;
                nowColor.color = NColor;
                break;
            case 6:
                NColor = Color.green;
                nowColor.color = NColor;
                break;
            case 9:
                NColor = Color.blue;
                nowColor.color = NColor;
                break;
        }

        //���¹̳� ����
        staminaTimer += Time.deltaTime;
        if (staminaTimer > recoveryCycle)
        {
            
            if (NowStamina >= 100) NowStamina = 100;
            else NowStamina += recoveryAmount;
            staminaTimer = 0f;
        }

        StaminaGage.value = NowStamina;
        StaminaGage.maxValue = MaxStamina;

        //ü�¹� ���� PlayerMove���� ü�� ����
        HealthBar.value = NowHealth;
        HealthBar.maxValue = MaxHealth;
    }

    //���¹̳� ����
    public int PlayerStamina
    {
        get
        {
            return NowStamina;
        }
        set
        {
            NowStamina -= value;
        }


    }

    //ü�� �Ӽ�
    public int PlayerHp
    {
        get
        {
            return NowHealth;
        }
        set
        {
            NowHealth -= value;
            if (NowHealth < 0) NowHealth = 0;
        }
    }
    //����Ʈ ���� �����ϴ� �Լ�
    public bool PadeInScreen()
    {
        Color AColor = PadeScreen.GetComponent<Image>().color;
        AColor.a = 1f;
        StartCoroutine(PadeImage(AColor, PadeInTime));
        return true;
    }
    //����Ʈ �ƿ��� �����ϴ� �Լ�
    public bool PadeOutScreen()
    {
        Color AColor = PadeScreen.GetComponent<Image>().color;
        AColor.a = 0f;
        StartCoroutine(PadeImage(AColor, PadeOutTime));
        return true;
    }

    //�̹����� ���̵� ��/�ƿ� ȿ���� �ִ� �ڷ�ƾ EndColor: ������ ��, LimitTime: ���̵��� ������ �ɸ��� �ð�
    IEnumerator PadeImage(Color EndColor, float LimitTime)
    {
        float isTime = 0f;//�ð����� ģ��

        while(isTime <= LimitTime)
        {
            Color StartColor = PadeScreen.GetComponent<Image>().color;
            float NomalizedTime = isTime / LimitTime;

            PadeScreen.GetComponent<Image>().color = Color.Lerp(StartColor, EndColor, NomalizedTime);
            isTime += Time.deltaTime;
            yield return null;
        }
        
    }
}
