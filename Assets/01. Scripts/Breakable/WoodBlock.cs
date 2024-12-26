using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : Breakable
{
    //내구성 - 블럭의 HP
    private const float Fracture = 400f;
    private const int score = 1000;
    
    private Coroutine _coroutine = null;
    
    private float currentFracture;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int currentSprite;
    
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        currentFracture = Fracture;
        currentSprite = sprites.Length - 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void Break(Collision2D other)
    {
        float relativeVelocity = other.relativeVelocity.magnitude;

        float imfactEnergy = 0.5f * other.rigidbody.mass * relativeVelocity * relativeVelocity;
        
        currentFracture -= imfactEnergy;
        
        if (0 >= currentFracture)
        {
            spriteRenderer.sprite = null;
            
            if (_coroutine == null)
                _coroutine = StartCoroutine(PlayBreakAnimation());
            
            return;
        }
        
        if (currentFracture <= (Fracture / sprites.Length) * (currentSprite + 1))
        {
            currentSprite--;
            
            if (currentSprite >= 0)
                spriteRenderer.sprite = sprites[currentSprite];
        }
    }
    
    private IEnumerator PlayBreakAnimation()
    {
        rb.simulated = false;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        onDestoryBehaviour?.Invoke(score);
        
        yield return new WaitForSeconds(2f);
        
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Break(other);
    }
}