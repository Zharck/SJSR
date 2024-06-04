using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 这是一个简单的读取类，为了按钮调用方便这里把场景读取方法也放这里了
/// </summary>
public class LoadData : MonoBehaviour
{
    private Text text;

    public Scores SD;

    private void Start()
    {
        SD.LoadData();
        text = GetComponent<Text>();
        if (text != null) ShowData();
    }
    /// <summary>
    /// 展示数据
    /// </summary>
    private void ShowData()
    {
        int len = SD.SD.historyScores.Length;
        string newText = "游玩次数：" + SD.SD.gameCount + "\n";
        newText += "上一次得分：" + SD.SD.befScore + "\n";
        newText += "得分排行榜：\n";
        for (int tmp = 0; tmp < len; tmp++) newText += "第" + (tmp+1) + "名：" + SD.SD.historyScores[len - 1 - tmp] + "\n";
        text.text = newText;
    }
    /// <summary>
    /// 切换场景
    /// </summary>
    public void Load(string scneName)
    {
        SceneManager.LoadScene(scneName);
    }
}
