using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : IState
{
    public InGameState()
    {
        DataManager.Instance.stageDatas[DataManager.Instance.currentStage].currentScore = 0;
    }
    
    public void SendData()
    {
        throw new System.NotImplementedException();
    }

    public void LoadData()
    {
        throw new System.NotImplementedException();
    }
}
