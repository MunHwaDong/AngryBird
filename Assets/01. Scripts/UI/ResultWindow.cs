using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScore;
    
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private Image stars;

    [SerializeField] private Image highScoreStamp;
    
    [SerializeField] private AudioSource resultSound;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetResult()
    {
        PlayData currentStageData = DataManager.Instance.stageDatas[DataManager.Instance.currentStage];
        
        resultSound.Play();

        currentScore.text = "Score : " + currentStageData.currentScore.ToString();
        
        highScore.text = currentStageData.highScore.ToString();
    }
}
