using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// ����һ���򵥵Ķ�ȡ�࣬Ϊ�˰�ť���÷�������ѳ�����ȡ����Ҳ��������
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
    /// չʾ����
    /// </summary>
    private void ShowData()
    {
        int len = SD.SD.historyScores.Length;
        string newText = "���������" + SD.SD.gameCount + "\n";
        newText += "��һ�ε÷֣�" + SD.SD.befScore + "\n";
        newText += "�÷����а�\n";
        for (int tmp = 0; tmp < len; tmp++) newText += "��" + (tmp+1) + "����" + SD.SD.historyScores[len - 1 - tmp] + "\n";
        text.text = newText;
    }
    /// <summary>
    /// �л�����
    /// </summary>
    public void Load(string scneName)
    {
        SceneManager.LoadScene(scneName);
    }
}
