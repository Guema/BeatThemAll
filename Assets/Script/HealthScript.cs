using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;


public partial class HealthScript : MonoBehaviour
{
    [SerializeField] HealthBar _healthbar;
    [SerializeField] bool _isDead = false;
    [SerializeField] int _maxHealth = 100;
    [SerializeField, Range(0f, 1f)] float _currentHealthPercent = 1f;


    public bool isDead
    {
        get => _isDead;
    }

    public int maxHealth
    {
        get => _maxHealth;
    }

    public int currentHealth
    {
        get => Mathf.CeilToInt(((float)_maxHealth) * _currentHealthPercent);
        private set => _currentHealthPercent = (float)value / (float)_maxHealth;
    }

    private float toPercentHP(int value)
    {
        return (float)(value / _maxHealth);
    }

    [Button("Test Damage")]
    public void DealDamage(uint damage = 10)
    {
        if (_isDead)
            return;

        currentHealth -= (int)Mathf.Min(damage, currentHealth);
        _isDead = _currentHealthPercent == 0f;
        UpdateHealthBar(_currentHealthPercent);
    }

    [Button("Test Heal")]
    public void Heal(uint healthToRestore = 10)
    {
        if (_isDead)
            return;

        currentHealth += (int)MathF.Min(healthToRestore, maxHealth - currentHealth);
        UpdateHealthBar(_currentHealthPercent);
    }

    public void Kill()
    {

    }
    
    private void OnValidate()
    {
        UpdateHealthBar(_currentHealthPercent);
    }

    private void UpdateHealthBar(float normalized)
    {
        if (_healthbar != null)
        {
            _healthbar.SetHealthBar(normalized);
        }
    }

}
