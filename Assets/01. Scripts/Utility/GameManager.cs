using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private IDictionary<int, Breakable> breakables = new Dictionary<int, Breakable>();
    
    [NonSerialized] public TrajectoryQueue trajectoryQueue;

    public Animator collisionAnimation;
    
    private PlayData playData;

    public PlayData PlayData => playData;

    public delegate void OnFinishedInitBehaviours();
    public event OnFinishedInitBehaviours onFinishedInitBehaviours;

    void Awake()
    {
        trajectoryQueue = FindObjectOfType<TrajectoryQueue>();
     
        playData = new PlayData();
        
        InitStage();
    }

    private void InitStage()
    {
        Breakable[] breakObjs = FindObjectsOfType<Breakable>();

        foreach (var breakObj in breakObjs)
        {
            if (breakObj is Pig)
            {
                playData.currentEnemiesNum++;
                
                breakObj.onDestoryBehaviour += UpdateCurrentEnemiesNum;
                
                breakObj.onDestoryBehaviour += playData.UpdateCurrentScore;
                
                breakObj.onDestoryBehaviour += CheckEndGameCondition;
            }
            else
            {
                breakObj.onDestoryBehaviour += playData.UpdateCurrentScore;
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

    void UpdateCurrentEnemiesNum(int dummy)
    {
        playData.currentEnemiesNum--;
    }

    void CheckEndGameCondition(int dummy)
    {
        Debug.Log(playData.currentEnemiesNum);
        
        if (playData.currentEnemiesNum <= 0)
        {
            EventBus.Publish(EventType.ENDGAME);
        }
    }
}
