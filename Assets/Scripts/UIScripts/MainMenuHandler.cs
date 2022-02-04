using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UIScripts
{
    public class MainMenuHandler : MonoBehaviour
    {
        //Connected Menus
        [SerializeField] private MenuUIHandler uiHandler;
        [SerializeField] private GameObject nameInputMenu;
        [SerializeField] private TMP_Text welcomeText; 

        public void OnEnable()
        {
            welcomeText.text = "Welcome " + PlayerManager.Instance.activePlayer
                + ", Your current best score is: " + PlayerManager.Instance.bestScore;
        }

        public void StartGameButtonPressed()
        {
            SceneManager.LoadScene(1);
        }
        
        public void ChangeNameButtonPressed()
        {
            uiHandler.ChangeMenu(gameObject, nameInputMenu);
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
}
