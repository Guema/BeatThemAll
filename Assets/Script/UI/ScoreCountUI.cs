using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txt;
    [SerializeField] int _scorecount;

    void Awake()
    {
        _scorecount = 0;
    }

    void Start()
    {
        _txt = GetComponentInChildren<TextMeshProUGUI>();
        ScoreScript.instance.OnScoreChanged += UpdateScore;
    }

    public void SetLifecount(int lifecount)
    {
        _txt.text = lifecount.ToString();
    }

    public void SetScore(int score)
    {
        _scorecount = score;
        _txt.SetText(score.ToString());
    }

    private void OnValidate()
    {
        _txt = GetComponentInChildren<TextMeshProUGUI>();
        _txt.text = _scorecount.ToString();
    }

    void UpdateScore(int before, int after)
    {
        SetScore(after);
    }
}