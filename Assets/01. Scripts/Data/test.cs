using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    void Start()
    {
        this.AddComponent<Button>();
        
        this.GetComponent<Button>().onClick.AddListener(delegate
        {
            foreach (var instanceStageData in DataManager.Instance.stageDatas)
            {
                Debug.Log(instanceStageData.Key + " : " + instanceStageData.Value);
            }
        });
    }
}
