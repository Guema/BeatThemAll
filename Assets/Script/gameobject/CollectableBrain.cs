using UnityEngine;

public class CollectableBrain : MonoBehaviour
{
    [SerializeField] LayerMask _CanPickUp;
    [SerializeField] CollectableStats _stats;

    private int _usesCount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_CanPickUp == (_CanPickUp | (1 << col.gameObject.layer)))
        {
            PickUp(col);
        }
    }

    void Start()
    {
        _usesCount = _stats.usesAmount;
    }

    void PickUp(Collider2D col)
    {
        GameObject p = col.GetComponentInParent<PlayerTag>().gameObject;
        HealthScript h = p.GetComponent<HealthScript>();
        ScoreScript s = p.GetComponent<ScoreScript>();
        h.Heal(_stats.healAmount);
        //s.Add(_stats.scoreAmount);

        if (--_usesCount <= 0) GameObject.Destroy(this.gameObject);
    }
}