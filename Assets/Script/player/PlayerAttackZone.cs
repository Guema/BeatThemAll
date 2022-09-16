using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    [SerializeField] List<HealthScript> _targets;
    private GameObject _root;

    void Start()
    {
        _root = GetComponentInParent<HealthScript>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponentInParent<HealthScript>() == null) return;
        GameObject colRoot = col.GetComponentInParent<HealthScript>().gameObject;
        if (colRoot.GetInstanceID() == _root.GetInstanceID()) return;
        if (colRoot.tag == _root.tag) return;

        _targets.Add(colRoot.GetComponent<HealthScript>());
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponentInParent<HealthScript>() == null) return;
        GameObject colRoot = col.GetComponentInParent<HealthScript>().gameObject;
        if (colRoot.GetInstanceID() == _root.GetInstanceID()) return;
        if (colRoot.tag == _root.tag) return;

        _targets.Remove(col.GetComponentInParent<HealthScript>());
    }

    public List<HealthScript> GetTargets()
    {
        return _targets;
    }
}