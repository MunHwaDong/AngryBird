using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Rigidbody2D Rb { get => (_rb ?? (_rb = GetComponent<Rigidbody2D>())); }
    
    private Animator _animator;
    public Animator Animator { get => (_animator ?? (_animator = GetComponent<Animator>())); }
    
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer { get => (_spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>())); }
    
    private Dragable _bird;
    public Dragable Bird { get => (_bird ?? (_bird = GetComponent<Dragable>())); }
    
    private BirdSkillStrategy _skillStrategy;
    public BirdSkillStrategy SkillStrategy { get => (_skillStrategy ?? (_skillStrategy = GetComponent<BirdSkillStrategy>())); }

    private BirdAudioSource _birdAudioSource;
    public BirdAudioSource BirdAudioSource { get => (_birdAudioSource ?? (_birdAudioSource = GetComponentInChildren<BirdAudioSource>())); }
    
    private Coroutine _coroutine;
    
    public ICommand skillCommand;

    public delegate void OnCollisionEvent();
    public event OnCollisionEvent OnCollision;
    
    public delegate void OnInputBehviour();
    public event OnInputBehviour onInputBehviour;
    
    protected void Awake()
    {
        Rb.gravityScale = 0f;
    }

    public void WaitForPlayerInput(Vector3 dummy)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(WaitingInputCoroutine());
    }

    public void StopWaitingInputCoroutine()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }
    
    IEnumerator WaitingInputCoroutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skillCommand.Execute();
                onInputBehviour?.Invoke();
                break;
            }
            
            yield return null;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) return;
        
        var instance = GameManager.Instance;
        
        instance.ResolutionCollision(other);
        
        OnCollision?.Invoke();
    }
}
