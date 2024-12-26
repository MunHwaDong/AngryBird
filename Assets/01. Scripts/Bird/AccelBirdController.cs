using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBirdController : BirdController
{
    new void Awake()
    {
        base.Awake();

        skillCommand = new InputSkillCommand(this);
    }
}
