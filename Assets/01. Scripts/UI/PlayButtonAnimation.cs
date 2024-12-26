using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private const float amplitude = 20f;
    private const float frequency = 20f;
    
    private Vector2 _startPosition;
    
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        _startPosition = _rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }

    private void OnDisable()
    {
        _rectTransform.anchoredPosition = _startPosition;
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            float newY = Mathf.Sin(Time.time * frequency * Mathf.Deg2Rad) * amplitude;
            _rectTransform.anchoredPosition = new Vector2(_startPosition.x, _startPosition.y - newY);
            yield return null;
        }
    }
}
