using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    private Animator birdAnimator;
    private BirdController birdController;
    
    [SerializeField] private Animator CollisionAnimator;

    void Start()
    {
        birdAnimator = GetComponentInParent<Animator>();
        birdController = GetComponentInParent<BirdController>();

        birdController.OnCollision += PlayCollisionAnimation;
        birdController.OnCollision += PlayIdleAnimation;
        
        birdController.bird.OnShot += PlayFlyAnimation;
    }

    void PlayFlyAnimation(Vector3 dummy)
    {
        birdAnimator.Rebind();
        birdAnimator.Play("Fly");
    }
    
    void PlayIdleAnimation()
    {
        birdAnimator.Rebind();
        birdAnimator.Play("Idle");
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
