using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[SelectionBase]
[RequireComponent(typeof(HealthScript))]
public class UnitScript : MonoBehaviour, HealthScript.IAttackable, IHasHealth
{

    [SerializeField, ReadOnly] HealthScript _healthScript;
    HealthScript healthScript
    {
        get => _healthScript;
    }

    [SerializeField, BoxGroup("Graphics")] Animator _animator;
    [SerializeField, BoxGroup("Physics")] ColliderListener2D _hitboxListener2D;



#if UNITY_EDITOR
    /// <summary>
    /// Reset is called when the user hits the Reset button in the Inspector's
    /// context menu or when adding the component the first time.
    /// </summary>
    void Reset()
    {
        Init();
    }
#endif


    void Init()
    {
        _healthScript = GetComponent<HealthScript>();

        _animator = gameObject.transform.Find("Render")?.GetComponentInChildren<Animator>() ?? gameObject.GetComponentInChildren<Animator>();
        _hitboxListener2D = gameObject.transform.Find("Hitbox")?.GetComponentInChildren<ColliderListener2D>() ?? gameObject.GetComponentInChildren<ColliderListener2D>();
    }


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Init();
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

    }


}
