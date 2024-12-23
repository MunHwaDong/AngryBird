using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private IDictionary<int, Breakable> breakables = new Dictionary<int, Breakable>();

    public int currentEnermyCount = 0;

    public void Start()
    {
        //EventBus.RegisterEvent(EventType.STARTGAME, InitStage);
        
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
