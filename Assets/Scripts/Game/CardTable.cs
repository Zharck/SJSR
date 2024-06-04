using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// ���飨���ƣ�����
/// </summary>
public enum CardType
{
    None,
    ��,
    ��,
    ˧,
    ��
}

/// <summary>
/// ����׿
/// ���ڹ������п��ƵĴ�����״̬
/// ��Ϸ�߼�Ҳд��������
/// </summary>
public class CardTable : MonoBehaviour
{
    private float cardSize = 100;//���ƴ�С
    private CardType needType = CardType.None;//��¼��ǰҪѰ�ҵĿ�������
    private GameObject befCard = null; //���ڼ�¼��һ�����Ƶ���Ϸ���󣬷���δƥ����ȷʱ��ת
    private WaitForSeconds waitTime = new WaitForSeconds(1);//ƥ��ʧ��չʾʱ��
    
    public int row = 4;//��������
    public int col = 5;//��������
    public Vector2 screenSize = new Vector2(1920, 1080);//��Ļ��С
    public Vector2 tableSize = new Vector2(1600, 900);//��Ϸ�����С
    public Sprite[] cardSprits; //���������Ӧ��ͼ��
    public UnityEvent onMatchSucc;//�÷��ź�
    /// <summary>
    /// ��ʼ�����п��ƣ�����ƥ�䷽���󶨵����ư�ť�¼���
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
                newCard.GetComponent<RectTransform>().position = new Vector3(x - col/2.0f + 0.5f,y - row/2.0f + 0.5f, 0) * cardSize + (Vector3)screenSize/2.0f;//��Ļ�ռ�-����
            }
        }
    }

    /// <summary>
    /// ����ƥ��
    /// </summary>
    public void CheckType(CardType newType,GameObject newCard)
    {
        newCard.GetComponent<Button>().enabled = false;//��ֹ�ظ����
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
    /// ƥ��ɹ�ʱ���ã���Ե�������޷��ٵ��
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
    /// ƥ��ʧ��ʱ���ã��ȴ�һ��ʱ�䲢��ת
    /// </summary>
    IEnumerator MatchFail(GameObject card1, GameObject card2)
    {
        yield return waitTime;
        card1.GetComponent<CardFlip>().StartFlip();
        card2.GetComponent<CardFlip>().StartFlip();
        yield return waitTime;
        card1.GetComponent<Button>().enabled = true;//�ָ�Ϊ�ɵ��״̬
        card2.GetComponent<Button>().enabled = true;
    }
}
