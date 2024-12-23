using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : Breakable
{
    //내구성 - 블럭의 HP
    private const float Fracture = 400f;
    
    private float currentFracture;
    private SpriteRenderer spriteRenderer;
    private int currentSprite;
    
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        currentFracture = Fracture;
        currentSprite = sprites.Length - 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public override void Break(Collision2D other)
    {
        float relativeVelocity = other.relativeVelocity.magnitude;

        float imfactEnergy = 0.5f * other.rigidbody.mass * relativeVelocity * relativeVelocity;
        
        currentFracture -= imfactEnergy;
        
        if (0 >= currentFracture)
        {
            Destroy(gameObject);
            return;
        }
        
        if (currentFracture <= (Fracture / sprites.Length) * (currentSprite + 1))
        {
            currentSprite--;
            
            if (currentSprite >= 0)
                spriteRenderer.sprite = sprites[currentSprite];
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Break(other);
    }
}