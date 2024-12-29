using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private IDictionary<int, Breakable> breakables = new Dictionary<int, Breakable>();

    private AudioSource _endDirectingSound;
    
    [NonSerialized] public TrajectoryQueue trajectoryQueue;

    public Animator collisionAnimation;
    
    private int _currentEnemiesNum;

    public delegate void OnFinishedInitBehaviours();
    public event OnFinishedInitBehaviours onFinishedInitBehaviours;

    void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Stage 1-1")
            {
                trajectoryQueue = FindObjectOfType<TrajectoryQueue>();
                _endDirectingSound = GetComponent<AudioSource>();
                _currentEnemiesNum = 0;

                InitStage();
            }
        };
    }

    private void InitStage()
    {
        Breakable[] breakObjs = FindObjectsOfType<Breakable>();

        foreach (var breakObj in breakObjs)
        {
            if (breakObj is Pig)
            {
                _currentEnemiesNum++;
                
                breakObj.onDestoryBehaviour += UpdateCurrentEnemiesNum;
                
                breakObj.onDestoryBehaviour +=
                    DataManager.Instance.stageDatas[DataManager.Instance.currentStage].UpdateCurrentScore;
                
                breakObj.onDestoryBehaviour += CheckEndGameCondition;
            }
            else
            {
                breakObj.onDestoryBehaviour +=
                    DataManager.Instance.stageDatas[DataManager.Instance.currentStage].UpdateCurrentScore;
            }

            breakables.Add(breakObj.gameObject.GetInstanceID(), breakObj);
        }
        
        Debug.Log(_currentEnemiesNum);
        
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
        _currentEnemiesNum--;
    }

    void CheckEndGameCondition(int dummy)
    {
        if (_currentEnemiesNum <= 0)
        {
            StartCoroutine(EndDirectingCoroutine());
        }
    }

    IEnumerator EndDirectingCoroutine()
    {
        _endDirectingSound.Play();
        
        yield return new WaitForSeconds(4f);
        
        EventBus.Publish(EventType.ENDGAME);
    }
}
