using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private IDictionary<int, Breakable> breakables = new Dictionary<int, Breakable>();

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
            breakables.Add(breakObj.gameObject.GetInstanceID(), breakObj);
        }
    }

    public void ResolutionCollision(int instanceID)
    {
        if (breakables.ContainsKey(instanceID))
        {
            Breakable collider = breakables[instanceID];
        
            collider.Break();
        }
    }
}
