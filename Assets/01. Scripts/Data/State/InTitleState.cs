using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class InTitleState : IState
{
    public void SendData()
    {
        DataManager.Instance.dbReference.Child(DataManager.Instance.userInfo.userId.ToString())
            .Child(DataManager.Instance.currentStage)
            .SetValueAsync(DataManager.Instance.stageDatas[DataManager.Instance.currentStage].highScore.ToString());
    }

    public async void LoadData()
    {
        await DataManager.Instance.dbReference.Child(DataManager.Instance.userInfo.userId.ToString()).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                for (int i = 1; i <= snapshot.ChildrenCount; i++)
                {
                    int stageHighScore = int.Parse(snapshot.Child(String.Concat("1-", i.ToString())).Value.ToString());

                    if(DataManager.Instance.stageDatas.ContainsKey(string.Concat("1-", i.ToString())))
                        DataManager.Instance.stageDatas.Add(string.Concat("1-", i.ToString()), new PlayData(stageHighScore));
                }
            }
        });
    }
}
