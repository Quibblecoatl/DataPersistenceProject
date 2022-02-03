using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    private Coroutine _fading;
    
    public void StartFadeInFadeOut()
    {
        if (_fading != null) { return; }
        
        _canvasGroup = GetComponent<CanvasGroup>();
        
        _fading = StartCoroutine(FadeInAndOut(_canvasGroup, 0, 1, fadeInTime));
    }
    
    private IEnumerator FadeInAndOut(CanvasGroup canvGroup, float start, float end, float duration)
    {
        var time = 0f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, time / duration);
            
            yield return null;
        }
        
        // Start new coroutine if alpha is 0, otherwise end.
        if (canvGroup.alpha > 0)
        {
            _fading = StartCoroutine(FadeInAndOut(_canvasGroup, 1, 0, fadeOutTime));
        }
        else
        {
            _fading = null;
        }
        
    }
}
