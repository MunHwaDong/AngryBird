using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkillStrategy : BirdSkillStrategy
{
    private Vector2 _bombForce = new Vector2(10f, 10f);
    public override void BirdSkill(BirdController birdController)
    {
        if (_birdController == null) _birdController = birdController;

        Collider2D[] rb2d = Physics2D.OverlapCircleAll(transform.position, 2f);
        
        
    }
}
