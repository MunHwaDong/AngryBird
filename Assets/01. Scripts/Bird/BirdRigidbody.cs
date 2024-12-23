using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BirdRigidbody : MonoBehaviour
{
    private Dragable dragable;
    private Rigidbody2D rb;
    private BirdController birdController;
    
    private Coroutine coroutine;
    [SerializeField] private GameObject trajectorySprite;
    
    void Awake()
    {
        dragable = GetComponentInParent<Dragable>();
        birdController = GetComponentInParent<BirdController>();
        rb = GetComponentInParent<Rigidbody2D>();
        
        coroutine = null;

        dragable.OnShot += ApplyForce;
        birdController.OnCollision += StopDrawTrajectory;
    }

    void ApplyForce(Vector3 force)
    {
        rb.gravityScale = 1f;
        
        force = new Vector3(force.x, force.y, -1f);
        
        rb.AddForce(force, ForceMode2D.Impulse);

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        coroutine = StartCoroutine(DrawTrajectoryCoroutine());
    }

    IEnumerator DrawTrajectoryCoroutine() 
    {
        while (rb.velocity.magnitude > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            Instantiate(trajectorySprite, new Vector3(rb.position.x, rb.position.y, -1f), Quaternion.identity);
        }
    }

    void StopDrawTrajectory()
    {
        StopCoroutine(coroutine);
    }
}
