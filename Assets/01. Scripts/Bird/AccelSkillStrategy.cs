using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelSkillStrategy : BirdSkillStrategy
{
    private Vector2 _acceleration = new Vector2(15f, 0f);
    
    public override void BirdSkill(BirdController birdController)
    {
        if(_birdController == null)
            _birdController = birdController;
        
        _birdController.Rb.AddForceAtPosition(_acceleration, transform.position, ForceMode2D.Impulse);
    }
}
