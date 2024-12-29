using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToTitle : MonoBehaviour
{
    void Start()
    {
        DataManager.Instance.Transition(new InTitleState());
        
        DataManager.Instance.SendData();
        
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Title"));
    }
}
