using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    public delegate void OnDestoryBehaviour(int score);
    public OnDestoryBehaviour onDestoryBehaviour;
    
    public abstract void Break(Collision2D other);
}
