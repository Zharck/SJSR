using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ����Card��Light
/// </summary>
public class SetCardLight : MonoBehaviour
{
    public GameObject cardLight;//��Ե�����
    public bool isLighting = false;//����״̬
    /// <summary>
    /// ���ñ�Ե����ɫ,����״̬
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
