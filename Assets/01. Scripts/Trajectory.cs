using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.trajectoryQueue.Add(this);
    }
}
