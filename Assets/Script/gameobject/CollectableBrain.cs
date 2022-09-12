using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableBrain : MonoBehaviour
{
    [SerializeField] LayerMask CanPickUp;
    [SerializeField] UnityEvent OnPickUp;
    List<string> CanPickUpNames;

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (CanPickUp == (CanPickUp | (1 << col.gameObject.layer)))
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        Debug.Log("pickup");
        OnPickUp.Invoke();
    }
}
