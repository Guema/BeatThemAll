using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class UnitAttackZone : MonoBehaviour
{
    [SerializeField, ReadOnly, BoxGroup("Links")] UnitScript _unit;
    [SerializeField, BoxGroup("Links")] ColliderListener2D _AttackTriggerZone;

    [SerializeField, ReadOnly, BoxGroup] List<HealthScript> _targets;

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

    /// <summary>
    /// 
    /// </summary>
    void Init()
    {
        _unit = GetComponent<UnitScript>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _AttackTriggerZone.onTriggerEnter2D += OnAttackRangeEnter;
        _AttackTriggerZone.onTriggerExit2D += OnAttackRangeExit;
    }


    private void OnAttackRangeEnter(Collider2D col)
    {
        HealthScript healthScript = col.GetComponentInParent<HealthScript>();

        if (!healthScript || healthScript == GetComponent<HealthScript>())
            return;

        if (tag == healthScript.tag)
            return;

        _targets.Add(healthScript);
    }

    private void OnAttackRangeExit(Collider2D col)
    {
        HealthScript healthScript = col.GetComponentInParent<HealthScript>();

        if (!healthScript)
            return;

        _targets.RemoveAll(hs => hs == healthScript);
    }
}