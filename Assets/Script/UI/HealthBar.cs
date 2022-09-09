using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] bool _showBorderExt;
    [SerializeField] bool _showBorderInt;

    [SerializeField, HideInInspector] Image _borderExt;
    [SerializeField, HideInInspector] Image _borderInt;

    void Awake()
    {
        _slider.value = 1f;
        _showBorderExt = true;
        _showBorderInt = true;

        if (_slider != null)
        {
            _borderExt = GetComponentsInChildren<Image>()[0];
            _borderInt = GetComponentsInChildren<Image>()[1];
        }
    }

    public void SetHealthBar(float normalized)
    {
        _slider.value = normalized;
    }

    private void OnValidate()
    {
        if (_slider != null)
        {
            _borderExt = GetComponentsInChildren<Image>()[0];
            _borderInt = GetComponentsInChildren<Image>()[1];

            _borderExt.enabled = _showBorderExt;
            _borderInt.enabled = _showBorderInt;
        }
    }
}