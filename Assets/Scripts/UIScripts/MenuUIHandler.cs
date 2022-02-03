using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{

    public float duration;
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

