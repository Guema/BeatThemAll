using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField, BoxGroup] HealthScript trackedHealthScript;

    [SerializeField, BoxGroup("UI Render")] Slider _slider;
    [SerializeField, BoxGroup("UI Render")] bool _showBorderExt;
    [SerializeField, BoxGroup("UI Render")] bool _showBorderInt;
    [SerializeField, BoxGroup("UI Render"), CurveRange(0f, 0f, 1f, 1f, EColor.Red)] AnimationCurve _barSmoothing = new AnimationCurve();
    [SerializeField, BoxGroup("UI Render")] float _dampening = 1f;

    [SerializeField, HideInInspector] Image _borderExt;
    [SerializeField, HideInInspector] Image _borderInt;

    WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    Coroutine _currentRoutine = null;

    void Awake()
    {
        _slider.value = 1f;


    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Init();
        trackedHealthScript.OnCurrentHealthChanged += UpdateCurrentHealth;
    }

    void Init()
    {
        if (_slider != null)
        {

            var images = GetComponentsInChildren<Image>();

            _borderExt = images?[0];
            _borderInt = images?[1];

            _borderExt.enabled = _showBorderExt;
            _borderInt.enabled = _showBorderInt;
        }
    }

    public void SetHealthBar(float normalized)
    {
        _slider.value = normalized;
    }

    private void OnValidate()
    {
        Init();
    }

    void UpdateCurrentHealth(int previous, int current)
    {
        // IEnumerator SmoothUpdateOverTime()
        // {
        //     float startTime = Time.time;
        //     float endTime = Time.time + _dampening;
        //     float previous = _slider.value;

        //     do
        //     {
        //         var t = _barSmoothing.Evaluate(Time.time);
        //         _slider.value = Mathf.Lerp(0f, 1f, t * previous);
        //         yield return _waitForEndOfFrame;
        //     } while (Time.time < endTime);
        // }

        // if (_currentRoutine != null)
        // {
        //     StopCoroutine(_currentRoutine);
        // }
        // _currentRoutine = StartCoroutine(SmoothUpdateOverTime());

        _slider.value = (float)current / (float)trackedHealthScript.maxHealth;
    }
}