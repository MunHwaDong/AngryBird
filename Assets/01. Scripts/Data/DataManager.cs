using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using File = System.IO.File;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    private IState _state;

    private string DataLocalPath;
    private readonly string _userData = "UserData.json";
    
    public string currentStage;
    public DatabaseReference dbReference;
    public UserData userInfo;
    
    public IDictionary<string, PlayData> stageDatas = new Dictionary<string, PlayData>();
    public readonly int NumOfStage = 18;

    new void Awake()
    {
        base.Awake();

        dbReference = FirebaseDatabase.DefaultInstance.GetReference("users");
        DataLocalPath = Application.persistentDataPath + "\\";

        _state = new InTitleState();
        
        InitUserData();
    }

    private async void InitUserData()
    {
        string[] foundUserDataPath = Directory.GetFiles(DataLocalPath, _userData);

        if (foundUserDataPath.Length > 0)
        {
            string userData = File.ReadAllText(foundUserDataPath[0]);
            
            userInfo = JsonUtility.FromJson<UserData>(userData);
        }
        else
        {
            int newUserID = await GetNextUserID();
            
            File.WriteAllText(String.Concat(DataLocalPath, "UserData.json"), JsonConvert.SerializeObject(new UserData(newUserID), Formatting.Indented));
            
            userInfo = new UserData(newUserID);
        }
        
        LoadData();
    }

    private async Task<int> GetNextUserID()
    {
        int nextUserID = 1;

        try
        {
            DataSnapshot dataSnapshot = await dbReference.GetValueAsync();

            if (dataSnapshot.Exists)
            {
                nextUserID = (int)dataSnapshot.ChildrenCount + 1;
            }
        }
        catch (Exception e)
        {
            Debug.Log($"Get Error {e}");
        }
        
        return nextUserID;
    }

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
}
