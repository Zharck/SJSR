using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRowAndCol : MonoBehaviour
{
    private int row, col;

    public GameObject rowText;
    public GameObject colText;
    public GameObject cardTable;
    public GameObject scoreCount;
    /// <summary>
    /// 检查输入是否合法，且数值是否合适
    /// </summary>
    private bool CheckText()
    {
        if (int.TryParse(rowText.GetComponent<Text>().text, out row) &&
            int.TryParse(colText.GetComponent<Text>().text, out col))
        {
            Debug.Log(row+" "+col);
            return (row * col) % 2 == 0;
        }
            
        return false;
    }
    /// <summary>
    /// 点击按钮后调用，并重置文本框
    /// </summary>
    public void TryStartGame()
    {
        rowText.GetComponent<Text>().text = "";
        colText.GetComponent<Text>().text = "";
        if (CheckText())
        {
            cardTable.GetComponent<CardTable>().row = row;
            cardTable.GetComponent<CardTable>().col = col;
            cardTable.GetComponent<CardTable>().InitCards();
            scoreCount.GetComponent<ScoreCount>().Init();
            transform.gameObject.SetActive(false);
        }
    }

}
