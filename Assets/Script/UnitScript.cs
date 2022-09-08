using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[SelectionBase]
[RequireComponent(typeof(HealthScript))]
public class UnitScript : MonoBehaviour, IAttackable, IHasHealth
{

    [SerializeField, ReadOnly] HealthScript _healthScript;

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
    }


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Init();
    }


}
