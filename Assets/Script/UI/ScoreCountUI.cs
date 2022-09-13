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
        _txt = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetLifecount(int lifecount)
    {
        _txt.text = lifecount.ToString();
    }

    private void OnValidate()
    {
        _txt = GetComponentInChildren<TextMeshProUGUI>();
        _txt.text = _scorecount.ToString();
    }
}