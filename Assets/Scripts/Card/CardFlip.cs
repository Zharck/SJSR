using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//卡牌的状态：正面、背面、正在翻转
public enum CardState
{
    Front,
    Back,
    Fliping
}

public class CardFlip : MonoBehaviour
{
    public GameObject frontGamgeObject;       //卡牌正面
    public GameObject backGameObject;        //卡牌的背面
    public CardState cardState = CardState.Back;  //卡牌当前的状态
    public float flipTime = 0.3f; //1/2翻转时间

    /// <summary>
    /// 初始化卡牌角度，根据cardState
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

    //开始翻转
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
    /// 翻转到背面
    /// </summary>
    IEnumerator ToBack()
    {
        cardState = CardState.Fliping;
        for (float i = flipTime, frontY = 0; i >= 0; i -= Time.deltaTime)
        {
            frontY += 90 * Time.deltaTime / flipTime; 
            frontGamgeObject.transform.eulerAngles = new Vector3(0,frontY,0);
            yield return 0;//利用yield等一帧
        }
        frontGamgeObject.transform.eulerAngles = new Vector3(0, 90, 0);//规范最终位置
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
    /// 翻转到正面
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
