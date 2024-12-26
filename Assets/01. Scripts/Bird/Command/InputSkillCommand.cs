using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputSkillCommand : ICommand
{
    private BirdController _birdController;

    public InputSkillCommand(BirdController birdController)
    {
        _birdController = birdController;
        _birdController.Bird.OnShot += _birdController.WaitForPlayerInput;
        _birdController.OnCollision += _birdController.StopWaitingInputCoroutine;
        
        _birdController.onInputBehviour += Execute;
    }
    
    public void Execute()
    {
        _birdController.SkillStrategy.BirdSkill(_birdController);
    }
}
