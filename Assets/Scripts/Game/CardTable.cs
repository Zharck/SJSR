using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 方块（卡牌）类型
/// </summary>
public enum CardType
{
    None,
    我,
    好,
    帅,
    啊
}

/// <summary>
/// 卡牌卓
/// 用于管理所有卡牌的创建与状态
/// 游戏逻辑也写在这里面
/// </summary>
public class CardTable : MonoBehaviour
{
    private float cardSize = 100;//卡牌大小
    private CardType needType = CardType.None;//记录当前要寻找的卡牌类型
    private GameObject befCard = null; //用于记录上一个卡牌的游戏对象，方便未匹配正确时翻转
    private WaitForSeconds waitTime = new WaitForSeconds(1);//匹配失败展示时间
    
    public int row = 4;//卡牌行数
    public int col = 5;//卡牌列数
    public Vector2 screenSize = new Vector2(1920, 1080);//屏幕大小
    public Vector2 tableSize = new Vector2(1600, 900);//游戏桌面大小
    public Sprite[] cardSprits; //卡牌种类对应的图像
    public UnityEvent onMatchSucc;//得分信号
    /// <summary>
    /// 初始化所有卡牌，并将匹配方法绑定到卡牌按钮事件上
    /// </summary>
    public void InitCards()
    {
        cardSize = Mathf.Min(tableSize.x / col, tableSize.y / row);

        int x, y;
        bool[] visited = new bool[row * col];
        GameObject cardPrefab = Resources.Load<GameObject>("prefabs/Card");
        for (int i = row * col/2; i > 0; i--)
        {
            CardType newType = (CardType)Random.Range(1, 5);//
            for (int j = 0; j < 2; j++)
            {
                GameObject newCard = Instantiate(cardPrefab,this.transform);
                newCard.transform.localScale *= cardSize / 100;
                newCard.GetComponent<Button>().onClick.AddListener(() => CheckType(newType, newCard));
                newCard.transform.Find("front").GetComponent<Image>().sprite = cardSprits[(int)newType - 1];
                do
                {
                    x = Random.Range(0, col);
                    y = Random.Range(0, row);
                } while (visited[y * col + x]);
                visited[y * col + x] = true;
                newCard.GetComponent<RectTransform>().position = new Vector3(x - col/2.0f + 0.5f,y - row/2.0f + 0.5f, 0) * cardSize + (Vector3)screenSize/2.0f;//屏幕空间-覆盖
            }
        }
    }

    /// <summary>
    /// 卡牌匹配
    /// </summary>
    public void CheckType(CardType newType,GameObject newCard)
    {
        newCard.GetComponent<Button>().enabled = false;//防止重复点击
        if (needType == CardType.None)
        {
            needType = newType;
            befCard = newCard;
        }
        else
        {
            if (newType == needType)
            {
                StartCoroutine(MatchSucc(befCard, newCard));
            }
            else
            {
                StartCoroutine(MatchFail(befCard, newCard));
            }
            befCard = null;
            needType = CardType.None;
        }
    }

    /// <summary>
    /// 匹配成功时启用，边缘高亮并无法再点击
    /// </summary>
    IEnumerator MatchSucc(GameObject card1,GameObject card2)
    {
        onMatchSucc.Invoke();
        card1.GetComponent<Button>().enabled = false;
        card2.GetComponent<Button>().enabled = false;
        card1.GetComponent<SetCardLight>().ChangeLight();
        card2.GetComponent<SetCardLight>().ChangeLight();
        yield return 0;
    }

    /// <summary>
    /// 匹配失败时启用，等待一段时间并翻转
    /// </summary>
    IEnumerator MatchFail(GameObject card1, GameObject card2)
    {
        yield return waitTime;
        card1.GetComponent<CardFlip>().StartFlip();
        card2.GetComponent<CardFlip>().StartFlip();
        yield return waitTime;
        card1.GetComponent<Button>().enabled = true;//恢复为可点击状态
        card2.GetComponent<Button>().enabled = true;
    }
}
