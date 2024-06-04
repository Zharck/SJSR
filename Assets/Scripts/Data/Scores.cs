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
        public int newScore;//当前分数
        public int gameCount;//游戏次数
        public int befScore;//上把游戏的分数
        public int[] historyScores;//历史排行，从低到高
    }

    public ScoresData SD;

    /// <summary>
    /// 添加新得分,更新结构体
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
    /// 存档到StreamingAssets文件夹
    /// </summary>
    public void SaveData()
    {
        string json = JsonUtility.ToJson(SD);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "ScoresData.dat"), json);
    }
    /// <summary>
    /// 从StreamingAssets文件夹读取存档
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
    ///  从StreamingAssets文件夹删除存档
    /// </summary>
    public void Delete()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "ScoresData.dat");

        if (File.Exists(filePath)) File.Delete(filePath);
    }

}
