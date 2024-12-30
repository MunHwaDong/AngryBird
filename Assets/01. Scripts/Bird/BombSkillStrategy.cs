using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkillStrategy : BirdSkillStrategy
{
    private const float _explosionForce = 100f;
    private const float _explosionRadius = 30f;

    private const float _WaitForSeconds = 1.5f;
    private const float _shakeTime = 0.5f;
    
    public override void BirdSkill(BirdController birdController)
    {
        if (_birdController == null) _birdController = birdController;

        _birdController.Rb.mass = _birdController.Rb.mass * 10f;

        StartCoroutine(WaitExplosionTime());
    }

    IEnumerator WaitExplosionTime()
    {
        yield return StartCoroutine(ExplosionCoroutine());
    }

    IEnumerator ExplosionCoroutine()
    {
        float t = 0;

        while (t < _WaitForSeconds)
        {
            t += Time.deltaTime;

            Color beforeColor = _birdController.SpriteRenderer.color;
            
            _birdController.SpriteRenderer.color = Color.Lerp(beforeColor, Color.red, t);
            
            yield return null;
        }

        BombBirdController _bc = _birdController as BombBirdController;
        
        _bc.bomb.gameObject.SetActive(true);
        _bc.bomb.Play();
        
        Explosion();
        
        _bc.BirdAudioSource.PlaySkillAudio();
        _birdController.SpriteRenderer.color = Color.white;

        StartCoroutine(ShakeCameraCoroutine());
    }
    
    void Explosion()
    {
        Collider2D[] coll2Ds = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var collider2d in coll2Ds)
        {
            Rigidbody2D rb = collider2d.attachedRigidbody;

            if (rb != null)
            {
                Vector2 direction = (rb.transform.position - transform.position).normalized;
                
                float distance = Vector2.Distance(transform.position, rb.transform.position);

                float force = Mathf.Lerp(_explosionForce, 0,  distance / _explosionRadius);
                
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator ShakeCameraCoroutine()
    {
        float t = 0f;
        Vector3 camOriginPos = Camera.main.transform.position;
        
        while (t < _shakeTime)
        {
            t += Time.deltaTime;
            
            Camera.main.transform.position = Random.insideUnitSphere * 2f + camOriginPos;
            
            yield return null;
        }
        
        Camera.main.transform.position = camOriginPos;
        
        Destroy(this);
    }
}
