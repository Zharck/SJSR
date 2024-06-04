using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private WaitForSeconds waitTime = new WaitForSeconds(1);//交给协程计时用
    private int allScore;//一局最多得分
    private int timeLimit;//时间限制

    public int pairTime = 3;//每对卡片对应时间限制
    public GameObject cardTable;
    public Scores SD;

    /// <summary>
    /// 初始化各值，并监听配对完成的事件，启用倒计时协程
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
    /// 得分增加并刷新显示，得分满后结束游戏
    /// </summary>
    private void GetOnePoint()
    {
        SD.SD.newScore += 2;
        if (SD.SD.newScore == allScore) GameOver();
        UpdateText();
    }
    /// <summary>
    /// 刷新显示
    /// </summary>
    private void UpdateText()
    {
        string newText = "剩余时间：" + timeLimit / 60 + ":" + timeLimit % 60 + "\n";
        newText += "目前得分：" + SD.SD.newScore;
        GetComponent<Text>().text = newText;
    }
    /// <summary>
    /// 自动存档
    /// </summary>
    private void OnDisable()
    {
        SD.AddNewScore();
        SD.SaveData();
    }
    /// <summary>
    /// 发出提示、存档并开启返回Menu的协程
    /// </summary>
    private void GameOver()
    {
        if (timeLimit > 0) transform.Find("GameOver").GetComponent<Text>().text = "你赢了";
        transform.Find("GameOver").gameObject.SetActive(true);
        //SD.AddNewScore();//如果需要自动存档则可以把这两句再加入到onDisable中
        //SD.SaveData();
        StartCoroutine(GotoMenu());
    }
    /// <summary>
    /// 3秒后返回Menu
    /// </summary>
    IEnumerator GotoMenu()
    {
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// 倒计时，并在倒计时结束时结束游戏
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
