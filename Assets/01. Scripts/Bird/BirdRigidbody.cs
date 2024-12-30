using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BirdRigidbody : MonoBehaviour
{
    private BirdController _birdController;
    private Coroutine coroutine;
    
    [SerializeField] private GameObject trajectorySprite;
    
    void Awake()
    {
        _birdController = GetComponentInParent<BirdController>();
        
        coroutine = null;

        _birdController.Bird.OnShot += ApplyForce;
        _birdController.OnCollision += StopDrawTrajectory;
    }

    void ApplyForce(Vector3 force)
    {
        _birdController.Rb.gravityScale = 1f;

        force /= _birdController.Rb.mass;
        
        force = new Vector2(force.x, force.y);
        
        _birdController.Rb.AddForce(force, ForceMode2D.Impulse);

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        coroutine = StartCoroutine(DrawTrajectoryCoroutine());
    }

    IEnumerator DrawTrajectoryCoroutine() 
    {
        while (_birdController.Rb.velocity.magnitude > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            Instantiate(trajectorySprite, new Vector3(_birdController.Rb.position.x, _birdController.Rb.position.y, -1f), Quaternion.identity);
        }
    }

    void StopDrawTrajectory()
    {
        StopCoroutine(coroutine);
    }
}
