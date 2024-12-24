using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    private BirdController _birdController;
    
    private Animator CollisionAnimator;

    void Start()
    {
        _birdController = GetComponentInParent<BirdController>();

        CollisionAnimator = GameManager.Instance.collisionAnimation;

        _birdController.OnCollision += PlayCollisionAnimation;
        _birdController.OnCollision += PlayIdleAnimation;
        
        _birdController.onInputBehviour += PlaySkillAnimation;
        
        _birdController.Bird.OnShot += PlayFlyAnimation;
    }

    void PlayFlyAnimation(Vector3 dummy)
    {
        _birdController.Animator.Rebind();
        _birdController.Animator.Play("Fly");
    }
    
    void PlayIdleAnimation()
    {
        _birdController.Animator.Rebind();
        _birdController.Animator.Play("Idle");
    }
    
    void PlaySkillAnimation()
    {
        _birdController.OnCollision -= PlayIdleAnimation;
        _birdController.OnCollision += () =>
        {
            _birdController.Animator.Rebind();
            _birdController.Animator.Play("Collision");
        };
        
        _birdController.Animator.Rebind();
        _birdController.Animator.Play("Skill");
    }

    void PlayCollisionAnimation()
    {
        StartCoroutine(CollisionAnimationCoroutine());
    }

    IEnumerator CollisionAnimationCoroutine()
    {
        CollisionAnimator.gameObject.SetActive(true);
        
        Vector3 aniPosition = transform.position;
        
        CollisionAnimator.transform.position = new Vector3(aniPosition.x, aniPosition.y, aniPosition.z - 2);
        
        CollisionAnimator.Play("Collision");
        
        yield return new WaitForSeconds(1f);
        
        CollisionAnimator.gameObject.SetActive(false);
    }
}
