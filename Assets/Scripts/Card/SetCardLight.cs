using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 管理Card的Light
/// </summary>
public class SetCardLight : MonoBehaviour
{
    public GameObject cardLight;//边缘光对象
    public bool isLighting = false;//激活状态
    /// <summary>
    /// 设置边缘光颜色,依照状态
    /// </summary>
    public void ChangeLight()
    {
        if (!isLighting)
        {
            cardLight.GetComponent<Image>().color = Color.yellow;
            isLighting = true;
        }
        else
        {
            cardLight.GetComponent<Image>().color = Color.green;
            isLighting = false;
        }
    }

    private void Start()
    {
        cardLight.GetComponent<Image>().color = Color.green;
        isLighting = false;
    }
}
