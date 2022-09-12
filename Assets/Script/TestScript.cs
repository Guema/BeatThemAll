using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;


class TestScript : MonoBehaviour
{
    [SerializeField, Foldout("Tracked"), ReadOnly] HealthScript healthScript;



    /// <summary>
    /// Reset is called when the user hits the Reset button in the Inspector's
    /// context menu or when adding the component the first time.
    /// </summary>
    void Reset()
    {
        Init();
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        Init();
    }

    void Init()
    {
        healthScript = GetComponent<HealthScript>();
    }

    [Button]
    void Deal10Damage()
    {
        healthScript.DealDamage(10);
    }

    [Button]
    void Heal10Life()
    {
        healthScript.Heal(10);
    }

}