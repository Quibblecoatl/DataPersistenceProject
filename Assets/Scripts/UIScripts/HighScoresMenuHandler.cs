using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoresMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject highScoreContainer;
    [SerializeField] private GameObject highScoreTextPrefab;
    private List<PlayerManager.PlayerData> highscoreList;

    private void Start()
    {
        highscoreList = new List<PlayerManager.PlayerData>();
        highscoreList = PlayerManager.Instance.giveOrderedByScore();
        StartCoroutine(CreateHighScoreList());
    }

    private IEnumerator CreateHighScoreList()
    {
        int i = 0;
        
        for (i = 0; i < 10; i++)
        {
            if (highscoreList.Count == i) { break; }
            
            var textObject = Instantiate(
                highScoreTextPrefab, 
                transform.position, 
                Quaternion.identity,
                highScoreContainer.transform);
            yield return null;
            var text = textObject.GetComponent<TMP_Text>();
            var time = 0f;

            text.text = $"{i + 1}. {highscoreList[i].playerName} : {highscoreList[i].highScore}";
            
            while (text.alpha < 1)
            {
                time += Time.deltaTime;
                text.alpha = Mathf.Lerp(0, 1, time * 2f);
                yield return null;
            }
            
        }
    }
    
    public void MenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ReplayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ExitGameButtonPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
