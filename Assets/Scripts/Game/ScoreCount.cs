using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private WaitForSeconds waitTime = new WaitForSeconds(1);//����Э�̼�ʱ��
    private int allScore;//һ�����÷�
    private int timeLimit;//ʱ������

    public int pairTime = 3;//ÿ�Կ�Ƭ��Ӧʱ������
    public GameObject cardTable;
    public Scores SD;

    /// <summary>
    /// ��ʼ����ֵ�������������ɵ��¼������õ���ʱЭ��
    /// </summary>
    public void Init()
    {
        SD.SD.newScore = 0;
        allScore = cardTable.GetComponent<CardTable>().row * cardTable.GetComponent<CardTable>().col;
        timeLimit = allScore * pairTime;
        UpdateText();
        FindObjectOfType<CardTable>().onMatchSucc.AddListener(()=>GetOnePoint());
        StartCoroutine(CountDown());
    }
    /// <summary>
    /// �÷����Ӳ�ˢ����ʾ���÷����������Ϸ
    /// </summary>
    private void GetOnePoint()
    {
        SD.SD.newScore += 2;
        if (SD.SD.newScore == allScore) GameOver();
        UpdateText();
    }
    /// <summary>
    /// ˢ����ʾ
    /// </summary>
    private void UpdateText()
    {
        string newText = "ʣ��ʱ�䣺" + timeLimit / 60 + ":" + timeLimit % 60 + "\n";
        newText += "Ŀǰ�÷֣�" + SD.SD.newScore;
        GetComponent<Text>().text = newText;
    }
    /// <summary>
    /// �Զ��浵
    /// </summary>
    private void OnDisable()
    {
        SD.AddNewScore();
        SD.SaveData();
    }
    /// <summary>
    /// ������ʾ���浵����������Menu��Э��
    /// </summary>
    private void GameOver()
    {
        if (timeLimit > 0) transform.Find("GameOver").GetComponent<Text>().text = "��Ӯ��";
        transform.Find("GameOver").gameObject.SetActive(true);
        //SD.AddNewScore();//�����Ҫ�Զ��浵����԰��������ټ��뵽onDisable��
        //SD.SaveData();
        StartCoroutine(GotoMenu());
    }
    /// <summary>
    /// 3��󷵻�Menu
    /// </summary>
    IEnumerator GotoMenu()
    {
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// ����ʱ�����ڵ���ʱ����ʱ������Ϸ
    /// </summary>
    IEnumerator CountDown()
    {
        while(true)
        {
            yield return waitTime;
            timeLimit -= 1;
            if (timeLimit < 0)
            {
                GameOver();
                break;
            }
            UpdateText();
        }
    }
}
