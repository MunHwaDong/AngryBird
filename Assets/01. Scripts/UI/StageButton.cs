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

        _button.onClick.AddListener(() => { SceneManager.sceneLoaded += OnLoadedBehaviours; });
        
        _button.onClick.AddListener(() => SceneManager.LoadScene(String.Concat("Stage ", _text.text)));
    }

    void OnLoadedBehaviours(Scene scene, LoadSceneMode mode)
    {
        DataManager.Instance.currentStage = _text.text;

        if (!DataManager.Instance.stageDatas.ContainsKey(DataManager.Instance.currentStage))
        {
            DataManager.Instance.stageDatas[DataManager.Instance.currentStage] = new PlayData();
        }
        
        DataManager.Instance.stageDatas[DataManager.Instance.currentStage].currentScore = 0;
        DataManager.Instance.Transition(new InGameState());
    }
}
