using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    private BirdController _birdController;

    private Coroutine _lifeCoroutine = null;
    private Coroutine _observeCoroutine = null;
    
    private const float limitTime = 6f;
    
    void Start()
    {
        _birdController = GetComponentInParent<BirdController>();
        _birdController.OnCollision += LifeTime;
        _birdController.OnCollision += ObservingBirdMovement;
    }
    
    //어떤 종류의 충돌체인지 상관 없이 첫 충돌 후 리미트 타임만큼 지나면 자동으로 다음 Bird를 생성한다.
    private void LifeTime()
    {
        _birdController.OnCollision -= LifeTime;
        
        EventBus.Publish(EventType.COLLISION);
        
        if(_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);
        
        _lifeCoroutine = StartCoroutine(LifeTimer());
    }

    void ObservingBirdMovement()
    {
        if(_observeCoroutine != null) StopCoroutine(_observeCoroutine);
        
        _observeCoroutine = StartCoroutine(ObservingBirdMoveCoroutine());
    }

    IEnumerator ObservingBirdMoveCoroutine()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (_birdController.Rb.velocity.magnitude <= 0.01f) break;
        }
        
        StopCoroutine(_observeCoroutine);
        StopCoroutine(_lifeCoroutine);
        EventBus.Publish(EventType.ENDTURN);
        Destroy(this);
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(limitTime);
        
        StopCoroutine(_observeCoroutine);
        StopCoroutine(_lifeCoroutine);
        EventBus.Publish(EventType.ENDTURN);
        Destroy(this);
    }
}
