using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private TextMeshProUGUI ScoreText;
    
    [SerializeField] private ResultWindow ResultCanvas;

    private int score = 0;

    void Awake()
    {
        GameManager.Instance.onFinishedInitBehaviours += Init;
        
        EventBus.RegisterEvent(EventType.ENDGAME, ShowResult);
    }

    void Init()
    {
        foreach (var breakable in GameManager.Instance.GetBreakables())
        {
            breakable.onDestoryBehaviour += UpdateScoreText;
        }
    }

    void UpdateScoreText(int score)
    {
        this.score += score;
        ScoreText.text = $"Score : { this.score.ToString() }";
    }

    void ShowResult()
    {
        ResultCanvas.gameObject.SetActive(true);

        ResultCanvas.SetResult();
    }
}
