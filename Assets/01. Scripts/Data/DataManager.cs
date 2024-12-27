using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private IState _state;

    protected List<StageData> _stageDatas;
    
    protected PlayData _playData;
    
    public PlayData _PlayData => _playData;

    public void Transition(IState newState)
    {
        _state = newState;
    }

    public void SendData()
    {
        _state.SendData();
    }

    public void LoadData()
    {
        _state.LoadData();
    }

    public void ProcessData()
    {
        _state.ProcessData();
    }
}
