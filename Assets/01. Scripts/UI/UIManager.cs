using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private int score = 0;

    void Awake()
    {
        GameManager.Instance.onFinishedInitBehaviours += Init;
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
}
