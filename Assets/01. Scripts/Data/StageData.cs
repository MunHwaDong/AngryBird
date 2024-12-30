using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public Dictionary<string, PlayData> playDatas = new Dictionary<string, PlayData>();

    public StageData()
    {
        for (int i = 1; i <= DataManager.Instance.NumOfStage; i++)
        {
            playDatas.Add("1-" + i.ToString(), new PlayData());
        }
    }
}
