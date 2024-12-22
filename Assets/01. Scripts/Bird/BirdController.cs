using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        
        EventBus.RegisterEvent(EventType.SHOT, OnUseGravity);
    }

    void OnUseGravity()
    {
        rb.gravityScale = 1f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var instance = GameManager.Instance;
        
        instance.ResolutionCollision(other.gameObject.GetInstanceID());
    }
}
