using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    private BirdController _birdController;
    private Coroutine _coroutine;
    
    private const float limitTime = 5f;
    
    void Start()
    {
        _birdController = GetComponentInParent<BirdController>();
        _birdController.OnCollision += LifeTime;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(ObservingBirdMovement());
    }

    //어떤 종류의 충돌체인지 상관 없이 첫 충돌 후 리미트 타임만큼 지나면 자동으로 다음 Bird를 생성한다.
    private void LifeTime()
    {
        _birdController.OnCollision -= LifeTime;
        
        EventBus.Publish(EventType.COLLISION);
        
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(LifeTimer());
    }

    IEnumerator ObservingBirdMovement()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (_birdController.Rb.velocity.magnitude <= 0.5f) break;
        }
        
        EventBus.Publish(EventType.ENDTURN);
        Destroy(this);
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(limitTime);
        
        EventBus.Publish(EventType.ENDTURN);
        Destroy(this);
    }
}
