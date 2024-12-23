using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    [NonSerialized] public Dragable bird;

    public delegate void OnCollisionEvent();
    public event OnCollisionEvent OnCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bird = GetComponent<Dragable>();

        rb.gravityScale = 0f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var instance = GameManager.Instance;
        
        instance.ResolutionCollision(other);
        
        OnCollision?.Invoke();
    }
}
