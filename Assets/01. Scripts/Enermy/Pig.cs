using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Breakable
{
    //내구성 - HP
    private const float Fracture = 200f;

    private int score = 10000;
    
    private float currentFracture;
    private SpriteRenderer spriteRenderer;
    private int currentSprite;
    private Animator animator;
    
    private Rigidbody2D rb;
    
    private Coroutine _coroutine = null;
    
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite scoreSprite;

    void Start()
    {
        currentFracture = Fracture;
        currentSprite = sprites.Length - 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void Break(Collision2D other)
    {
        float relativeVelocity = other.relativeVelocity.magnitude;

        float imfactEnergy = 0.5f * other.rigidbody.mass * relativeVelocity * relativeVelocity;

        currentFracture -= imfactEnergy;
        
        if (0 >= currentFracture)
        {
            if(_coroutine == null)
                _coroutine = StartCoroutine(PlayDieAnimation());
            
            return;
        }
        
        if (currentFracture <= (Fracture / sprites.Length) * (currentSprite + 1))
        {
            currentSprite--;
            
            if (currentSprite >= 0)
                spriteRenderer.sprite = sprites[currentSprite];
        }
    }

    private IEnumerator PlayDieAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            rb.simulated = false;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            
            animator.Rebind();
            animator.Play("Die");
            
            onDestoryBehaviour?.Invoke(score);
        }
        
        yield return new WaitForSeconds(2f);
        
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Break(other);
    }
}
