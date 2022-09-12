using UnityEngine;

public class CollectableBrain : MonoBehaviour
{
    [SerializeField] LayerMask _CanPickUp;
    [SerializeField] CollectableStats _stats;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_CanPickUp == (_CanPickUp | (1 << col.gameObject.layer)))
        {
            PickUp(col);
        }
    }

    void PickUp(Collider2D col)
    {
        GameObject p = col.GetComponentInParent<PlayerTag>().gameObject;
        HealthScript h = p.GetComponent<HealthScript>();
        ScoreScript s = p.GetComponent<ScoreScript>();
        h.Heal(_stats.healAmount);
        //s.Add(_stats.scoreAmount);
        GameObject.Destroy(this.gameObject);
    }
}