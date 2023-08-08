using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSizeChangeObj : MonoBehaviour
{
    public Color ClearColor;
    public GameObject LinkObj;

    public Vector2 TartgetSize; //Ÿ�� ������

    public float duration = 2f; // ���ϴµ� �ɸ��� �ð�

    public bool isReColoring;

    Color objColor;
    // Start is called before the first frame update
    void Start()
    {
        objColor = this.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        //objColor�� ������Ʈ�� �������� �ٲٱ�
        gameObject.GetComponent<SpriteRenderer>().color = objColor;
    }

    //��ĥ���� �� �۵��ϴ� �Լ�
    public void ActAction(Color nowColor)
    {
        if (objColor == ClearColor && !isReColoring)
        {
            return;
        }
        else
        {   
            objColor = nowColor;
            //����ġ �� ������Ʈ�� ũ�Ⱑ ���ϴ� �̺�Ʈ ó��
            if (objColor == ClearColor)
                StartCoroutine(ScaleOverTime());
            return;
        }
    }

    //�ܺο��� ������ Ŭ���� ��Ű�� �Լ�
    public void ForcedClear()
    {
        objColor = ClearColor;
        StartCoroutine(ScaleOverTime());
    }

    //Ư�� ������Ʈ�� ������ ������ ������ ��ü�ϴ� �ڷ�ƾ
    private IEnumerator ScaleOverTime()
    {
        float StartTime = Time.time;
        Vector2 StartSize = LinkObj.transform.localScale;

        while (Time.time - StartTime < duration)
        {
            float progress = (Time.time - StartTime) / duration;
            Vector2 currentSize = Vector2.Lerp(StartSize, TartgetSize, progress);
            LinkObj.transform.localScale = currentSize;
            yield return null; // 1 ������ ���
        }

        LinkObj.transform.localScale = TartgetSize; // ��Ȯ�� ũ��� ����
    }
}
