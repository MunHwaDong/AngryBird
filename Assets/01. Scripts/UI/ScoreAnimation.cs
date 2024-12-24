using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    private Breakable breakableObj;
    
    private Coroutine _coroutine;

    private const float animationSpeed = 2.5f;
    private const float playTime = 1f;
    
    void Start()
    {
        breakableObj = GetComponentInParent<Breakable>();
        breakableObj.onDestoryBehaviour += PlayScoreAnimation;
    }

    void PlayScoreAnimation(int dummy)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(PlayScoreAnimationCoroutine());
    }

    IEnumerator PlayScoreAnimationCoroutine()
    {
        gameObject.SetActive(true);
        
        //transform.localScale = new Vector3(1, 1, 1);
        transform.parent.localScale = new Vector3(1, 1, 1);;
        
        float percent = 0;

        while (percent < playTime)
        {
            percent += Time.deltaTime * animationSpeed;
            
            transform.localScale = Vector3.Lerp(new Vector3(0f, 0f, 1f), new Vector3(10f, 10f, 1f), percent / playTime);
            
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);
    }
}
