using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Button _button;

    void Start()
    {
        _button = GetComponentInParent<Button>();
        _text = GetComponent<TextMeshProUGUI>();
        
        DataManager.Instance.currentStage = _text.text;

        if (!DataManager.Instance.stageDatas.ContainsKey(DataManager.Instance.currentStage))
        {
            DataManager.Instance.stageDatas[DataManager.Instance.currentStage] = new PlayData();
        }
        
        DataManager.Instance.Transition(new InGameState());
        
        _button.onClick.AddListener(() => SceneManager.LoadScene(String.Concat("Stage ", _text.text)));
    }
}
