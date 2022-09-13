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
        // get comp
        GameObject p = col.GetComponentInParent<PlayerTag>().gameObject;
        HealthScript h = p.GetComponent<HealthScript>();

        // heal
        if (_stats.healAmount > 0)
        {
            if (h.currentHealth == h.maxHealth && _stats.scoreAmount == 0) return; // do not consum item

            if (h.currentHealth != h.maxHealth)
            {
                h.Heal(_stats.healAmount);
            }
        }

        // score
        if (_stats.scoreAmount > 0)
        {
            ScoreScript.instance.AddScore((uint)_stats.scoreAmount);
        }

        // destroy
        if (--_usesCount <= 0) GameObject.Destroy(this.gameObject);
    }
}