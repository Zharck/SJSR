using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scores", menuName = "Data/Scores")]
public class Scores : ScriptableObject
{
    [System.Serializable]
    public struct ScoresData
    {
        public int newScore;//��ǰ����
        public int gameCount;//��Ϸ����
        public int befScore;//�ϰ���Ϸ�ķ���
        public int[] historyScores;//��ʷ���У��ӵ͵���
    }

    public ScoresData SD;

    /// <summary>
    /// ����µ÷�,���½ṹ��
    /// </summary>
    public void AddNewScore()
    {
        int tmp = SD.historyScores.Length;
        SD.gameCount++;
        SD.befScore = SD.newScore;
        if (tmp < 10)
        {
            System.Array.Resize(ref SD.historyScores, tmp + 1);
            SD.historyScores[tmp] = SD.newScore;
            System.Array.Sort(SD.historyScores);
        }
        else if (SD.historyScores[0] < SD.newScore)
        {
            SD.historyScores[0] = SD.newScore;
            System.Array.Sort(SD.historyScores);
        }
        SD.newScore = 0;
    }

    /// <summary>
    /// �浵��StreamingAssets�ļ���
    /// </summary>
    public void SaveData()
    {
        string json = JsonUtility.ToJson(SD);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "ScoresData.dat"), json);
    }
    /// <summary>
    /// ��StreamingAssets�ļ��ж�ȡ�浵
    /// </summary>
    public void LoadData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "ScoresData.dat");
        if (File.Exists(filePath))
        {
            string jsonFromFile = File.ReadAllText(filePath);
            SD = JsonUtility.FromJson<ScoresData>(jsonFromFile);
        }
    }
    /// <summary>
    ///  ��StreamingAssets�ļ���ɾ���浵
    /// </summary>
    public void Delete()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "ScoresData.dat");

        if (File.Exists(filePath)) File.Delete(filePath);
    }

}
