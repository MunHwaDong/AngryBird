using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    private Breakable breakableObj;

    private const float animationSpeed = 2.5f;
    private const float playTime = 1f;
    
    void Start()
    {
        breakableObj = GetComponentInParent<Breakable>();
        breakableObj.onDestoryBehaviour += PlayScoreAnimation;
    }

    void PlayScoreAnimation()
    {
        StartCoroutine(PlayScoreAnimationCoroutine());
    }

    IEnumerator PlayScoreAnimationCoroutine()
    {
        gameObject.SetActive(true);
        
        float percent = 0;

        while (percent < playTime)
        {
            percent += Time.deltaTime * animationSpeed;
            
            transform.localScale = Vector3.Lerp(new Vector3(0f, 0f, 1f), new Vector3(2f, 2f, 1f), percent / playTime);
            
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);
    }
}
