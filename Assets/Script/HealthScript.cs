using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnitScript))]
public partial class HealthScript : MonoBehaviour
{
    [SerializeField] bool _isDead = false;
    [SerializeField] int _maxHealth = 100;
    [SerializeField, Range(0f, 1f)] float _currentHealthPercent = 1f;

    public event UnityAction<int, int> OnCurrentHealthChanged;
    public event UnityAction<int, int> OnMaxHealthChanged;
    public event UnityAction<int> OnHealthReset;
    public event UnityAction OnDie;

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

    public void DealDamage(int damage)
    {
        if (_isDead)
            return;
        int previousHealth = currentHealth;
        currentHealth -= (int)Mathf.Min(Mathf.Abs(damage), currentHealth);
        _isDead = _currentHealthPercent == 0f;

        OnCurrentHealthChanged?.Invoke(previousHealth, currentHealth);
        if (isDead)
            OnDie?.Invoke();
    }

    public void Heal(int healthToRestore)
    {
        if (_isDead)
            return;

        int previousHealth = currentHealth;
        currentHealth += (int)MathF.Min(Mathf.Abs(healthToRestore), maxHealth - currentHealth);

        OnCurrentHealthChanged?.Invoke(previousHealth, currentHealth);
    }

    public void Kill()
    {
        int previousHealth = currentHealth;
        _isDead = true;
        currentHealth = 0;

        OnCurrentHealthChanged?.Invoke(previousHealth, currentHealth);
        OnDie?.Invoke();
    }

}
