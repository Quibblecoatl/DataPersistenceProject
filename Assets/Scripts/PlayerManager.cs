using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [HideInInspector] public string activePlayer;
    [HideInInspector] public int bestScore = 0;
    public Dictionary<string, int> HighScoreDictionary;
    [HideInInspector] public PlayerList highScoreList;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        highScoreList.playerList = new List<PlayerData>();
        HighScoreDictionary = new Dictionary<string, int>();
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }
    
    [Serializable]
    public struct PlayerData
    {
        public string playerName;
        public int highScore;

        public PlayerData(string name, int score)
        {
            playerName = name;
            highScore = score;
        }
    }

    [Serializable]
    public struct PlayerList
    {
        public List<PlayerData> playerList;
    }

    public void setPlayerData(string player)
    {
        activePlayer = player;
        if (HighScoreDictionary.ContainsKey(activePlayer))
        {
            bestScore = HighScoreDictionary[player];
        }
        else
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        // If our Player doesn't exist in the dictionary, add them.
        if (!HighScoreDictionary.ContainsKey(activePlayer))
        {
            HighScoreDictionary.Add(activePlayer, bestScore);
        }
        else
        {
            // If our best score is better than our stored score, update it
            if (HighScoreDictionary[activePlayer] < bestScore)
            {
                HighScoreDictionary[activePlayer] = bestScore;
            }
            // If our player exists and their score is lower then their best, there is no point in saving, return
            else
            {
                return;
            }
        }
        
        PlayerList tempList = new PlayerList();
        tempList.playerList = new List<PlayerData>();
        
        foreach (KeyValuePair<string, int> keyValuePair in HighScoreDictionary)
        {
            PlayerData player = new PlayerData(keyValuePair.Key, keyValuePair.Value);
            tempList.playerList.Add(player);
        }

        string json = JsonUtility.ToJson(tempList);
        
        Debug.Log(json);
        
        File.WriteAllText(path, json);
    }

    public void loadBestScore()
    {
        bestScore = HighScoreDictionary[activePlayer];
    }
    
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            if (json == String.Empty) { return; }
            
            PlayerList datalist = JsonUtility.FromJson<PlayerList>(json);

            highScoreList = datalist;

            foreach (var item in datalist.playerList.Where(item => !HighScoreDictionary.ContainsKey(item.playerName)))
            {
                HighScoreDictionary.Add(item.playerName, item.highScore);
            }
            
            if (HighScoreDictionary.ContainsKey(activePlayer))
            {
                loadBestScore();
            }
        }
    }
}
