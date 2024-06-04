using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ƶ�״̬�����桢���桢���ڷ�ת
public enum CardState
{
    Front,
    Back,
    Fliping
}

public class CardFlip : MonoBehaviour
{
    public GameObject frontGamgeObject;       //��������
    public GameObject backGameObject;        //���Ƶı���
    public CardState cardState = CardState.Back;  //���Ƶ�ǰ��״̬
    public float flipTime = 0.3f; //1/2��תʱ��

    /// <summary>
    /// ��ʼ�����ƽǶȣ�����cardState
    /// </summary>
    private void Init()
    {
        if (cardState == CardState.Front)
        {
            frontGamgeObject.transform.eulerAngles = Vector3.zero;
            backGameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            backGameObject.transform.eulerAngles = Vector3.zero;
            frontGamgeObject.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }

    private void Start()
    {
        Init();
    }

    //��ʼ��ת
    public void StartFlip()
    {
        if (cardState == CardState.Front)
        {
            StartCoroutine(ToBack());
        }
        else if (cardState == CardState.Back)
        {
            StartCoroutine(ToFront());
        }   
    }

    /// <summary>
    /// ��ת������
    /// </summary>
    IEnumerator ToBack()
    {
        cardState = CardState.Fliping;
        for (float i = flipTime, frontY = 0; i >= 0; i -= Time.deltaTime)
        {
            frontY += 90 * Time.deltaTime / flipTime; 
            frontGamgeObject.transform.eulerAngles = new Vector3(0,frontY,0);
            yield return 0;//����yield��һ֡
        }
        frontGamgeObject.transform.eulerAngles = new Vector3(0, 90, 0);//�淶����λ��
        for (float i = flipTime, backY = 90; i >= 0; i -= Time.deltaTime)
        {
            backY -= 90 * Time.deltaTime / flipTime;
            backGameObject.transform.eulerAngles = new Vector3(0, backY, 0);
            yield return 0;
        }
        backGameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        cardState = CardState.Back;
    }

    /// <summary>
    /// ��ת������
    /// </summary>
    IEnumerator ToFront()
    {
        cardState = CardState.Fliping;
        for (float i = flipTime, backY = 0; i >= 0; i -= Time.deltaTime)
        {
            backY += 90 * Time.deltaTime / flipTime;
            backGameObject.transform.eulerAngles = new Vector3(0, backY, 0);
            yield return 0;
        }
        backGameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        for (float i = flipTime, frontY = 90; i >= 0; i -= Time.deltaTime)
        {
            frontY -= 90 * Time.deltaTime / flipTime;
            frontGamgeObject.transform.eulerAngles = new Vector3(0, frontY, 0);
            yield return 0;
        }
        frontGamgeObject.transform.eulerAngles = new Vector3(0, 0, 0);
        cardState = CardState.Front;
    }
}
