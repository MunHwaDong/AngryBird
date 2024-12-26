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
        
        _button.onClick.AddListener(() => SceneManager.LoadScene(String.Concat("Stage ", _text.text)));
    }
}
