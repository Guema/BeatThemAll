using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class ScoreScript : MonoBehaviour
{
    public delegate void ScoreChangedCallback(int before, int after);
    public delegate void ScoreResetedCallback(int newValue);

    private static ScoreScript _instance = null;

    [SerializeField, ReadOnly]
    private uint _score;
    /// <summary>
    /// Callback handler called only when Score is increased;
    /// </summary>
    public event ScoreChangedCallback OnScoreIncreased;
    /// <summary>
    /// Callback handler called when Score was changed in any way;
    /// </summary>
    public event ScoreChangedCallback OnScoreChanged;
    /// <summary>
    /// Callback handler called when Score was decreased;
    /// </summary>
    public event ScoreChangedCallback OnScoreDecreased;
    /// <summary>
    /// Callback handler called when Score was reseted;
    /// </summary>
    public event ScoreResetedCallback OnScoreReseted;

    public static ScoreScript instance
    {
        get => instance;
    }

    public int score
    {
        get => (int)_score;
    }

#if UNITY_EDITOR

    void Reset() => Init();
    void Validate() => Init();

#endif


    void Init()
    {
        _instance ??= this;

        if (_instance != this)
        {
            DestroyImmediate(this);
            throw new System.Exception($@"ScoreScript already exists. Only one allowed. 
            Use 'ScoreScript.instance' to get the current ScoreScript");
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Init();
    }

    public void AddScore(uint scoreToAdd)
    {
        uint previous_score = _score;
        _score += scoreToAdd;

        OnScoreIncreased.Invoke((int)previous_score, (int)_score);
        OnScoreChanged.Invoke((int)previous_score, (int)_score);

    }

    public void SubstractScore(uint scoreToSubstract)
    {
        uint previous_score = _score;
        this._score -= scoreToSubstract;

        OnScoreDecreased.Invoke((int)previous_score, (int)_score);
        OnScoreChanged.Invoke((int)previous_score, (int)_score);
    }

    public void ResetScore(uint score = 0)
    {
        this._score = score;

        OnScoreReseted.Invoke((int)score);
    }


}
