using System;
using System.Collections;
using UnityEngine;

namespace UIScripts
{
    public class MenuUIHandler : MonoBehaviour
    {
        [SerializeField] private MainMenuHandler mainMenu;
        [SerializeField] private GameObject nameInputMenu;
        
        public float duration;

        private void Start()
        {
            if (PlayerManager.Instance.activePlayer != "")
            {
                mainMenu.gameObject.SetActive(true);
            }
            else
            {
                nameInputMenu.gameObject.SetActive(true);
            }
        }

        public void ChangeMenu(GameObject menuToDisable, GameObject menuToEnable)
        {
            StartCoroutine(SetFade(menuToDisable, 1, 0, menuToEnable));
        }

        private IEnumerator SetFade(GameObject fadeObject, float start, float end, GameObject toActivate = default(GameObject))
        {
            var canvasGroup = fadeObject.GetComponent<CanvasGroup>();
            var time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
            
                canvasGroup.alpha = Mathf.Lerp(start, end, time / duration);
                yield return null;
            }

            if (toActivate != null)
            {
                fadeObject.SetActive(false);
                toActivate.SetActive(true);
                StartCoroutine(SetFade(toActivate, 0, 1));
            }
        }
    
    }
}

