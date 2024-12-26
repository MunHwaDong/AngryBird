using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSkillCommand : ICommand
{
    private BirdController _birdController;

    public CollisionSkillCommand(BirdController birdController)
    {
        _birdController = birdController;

        _birdController.OnCollision += Execute;
    }

    public void Execute()
    {
        _birdController.SkillStrategy.BirdSkill(_birdController);
    }
}
