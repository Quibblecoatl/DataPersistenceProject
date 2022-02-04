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
    [HideInInspector] public PlayerList highScoreList;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        highScoreList.playerList = new List<PlayerData>();
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
        var playerData = findPlayerInList();
        
        if (playerData != null)
        {
            bestScore = playerData.Value.highScore;
        }
        else
        {
            bestScore = 0;
            SaveData();
        }
    }

    public PlayerData? findPlayerInList()
    {
        foreach (var player in highScoreList.playerList.Where(player => player.playerName == activePlayer))
        {
            return player;
        }
        return null;
    }

    public List<PlayerData> giveOrderedByScore()
    {
        return highScoreList.playerList.OrderByDescending(player => player.highScore).ToList();
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        var player = findPlayerInList();

        if (player != null)
        {
            if (bestScore > player.Value.highScore)
            {
                highScoreList.playerList.Remove(player.Value);
            }
            else
            {
                return;
            }
        }

        PlayerData newPlayerEntry = new PlayerData(activePlayer, bestScore);
        highScoreList.playerList.Add(newPlayerEntry);


        string json = JsonUtility.ToJson(highScoreList);

        File.WriteAllText(path, json);
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
        }
    }
}
