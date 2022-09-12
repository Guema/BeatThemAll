using UnityEngine;
using UnityEngine.Events;

public class CollectableBrain : MonoBehaviour
{
    [SerializeField] LayerMask _CanPickUp;
    [SerializeField] CollectableStats _stats;
    //[SerializeField,Range(0,100)] int _healAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_CanPickUp == (_CanPickUp | (1 << col.gameObject.layer)))
        {
            PickUp(col);
        }
    }

    void PickUp(Collider2D col)
    {
        Debug.Log("pickup DONE");

        GameObject p = col.GetComponentInParent<PlayerTag>().gameObject;
        HealthScript h = p.GetComponent<HealthScript>();
        h.Heal(_stats.healAmount);
        GameObject.Destroy(this.gameObject);
    }
}