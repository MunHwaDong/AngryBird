using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBirdController : BirdController
{
    public ParticleSystem bomb;
    
    new void Awake()
    {
        base.Awake();

        skillCommand = new CollisionSkillCommand(this);
    }
}
