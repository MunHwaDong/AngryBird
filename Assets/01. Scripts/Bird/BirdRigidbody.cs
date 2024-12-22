using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRigidbody : MonoBehaviour
{
    private GameObject bird;
    
    private Dragable dragable;
    private Rigidbody2D rb;
    
    void Awake()
    {
        bird = transform.parent.gameObject;
        dragable = bird.GetComponent<Dragable>();
        rb = GetComponentInParent<Rigidbody2D>();

        dragable.OnShot += ApplyForce;
    }

    void ApplyForce(Vector3 force)
    {
        Debug.Log("shot " + force);

        rb.gravityScale = 1f;
        
        force = new Vector3(force.x, force.y, -1f);
        //rb.AddForce(force, ForceMode2D.Impulse);
        
        StartCoroutine(ApplyForceCoroutine(force));
    }

    IEnumerator ApplyForceCoroutine(Vector3 force)
    {
        float dt = 0.02f;
        
        Vector3 velocity = force / rb.mass;
        Vector3 position = bird.transform.position;
        
        while (true)
        {
            velocity += (velocity + (Physics.gravity / rb.mass)) * dt;
            position += velocity * dt;
        
            if (velocity.magnitude <= 0.01f)
            {
                break;
            }
            
            bird.transform.position = position;
            
            yield return null;
        }
    }
}
