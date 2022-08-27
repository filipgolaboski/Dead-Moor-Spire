using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreen : MonoBehaviour
{
    public CanvasGroup deathCanvas;
    public float speed;

    public void ShowDeathCanvas()
    {
        StartCoroutine(ChangeCanvasOpacity(1f));
        
    }

    public void HideDeathCanvas()
    {
        StopAllCoroutines();
        deathCanvas.alpha = 0f;
    }

    IEnumerator ChangeCanvasOpacity(float target)
    {
        while(deathCanvas.alpha < target)
        {
            deathCanvas.alpha = Mathf.Lerp(deathCanvas.alpha, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
