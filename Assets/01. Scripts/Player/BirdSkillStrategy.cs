using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BirdSkillStrategy : MonoBehaviour
{
    protected BirdController _birdController;

    public abstract void BirdSkill(BirdController birdController);
}
