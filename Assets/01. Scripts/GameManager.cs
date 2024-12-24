using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private IDictionary<int, Breakable> breakables = new Dictionary<int, Breakable>();

    public Animator collisionAnimation;
    public int currentEnermyCount = 0;

    public delegate void OnFinishedInitBehaviours();
    public event OnFinishedInitBehaviours onFinishedInitBehaviours;

    void Start()
    {
        //base.Awake();

        InitStage();
    }

    private void InitStage()
    {
        Breakable[] breakObjs = FindObjectsOfType<Breakable>();

        foreach (var breakObj in breakObjs)
        {
            if (breakObj is Pig)
            {
                currentEnermyCount++;
            }
            
            breakables.Add(breakObj.gameObject.GetInstanceID(), breakObj);
        }
        
        onFinishedInitBehaviours?.Invoke();
    }

    public IEnumerable<Breakable> GetBreakables()
    {
        foreach (var VARIABLE in breakables)
        {
            yield return VARIABLE.Value;
        }
    }

    public void ResolutionCollision(Collision2D other)
    {
        int id = other.gameObject.GetInstanceID();
        
        if (breakables.ContainsKey(id))
        {
            Breakable collider = breakables[id];
        
            collider.Break(other);
        }
    }
}
