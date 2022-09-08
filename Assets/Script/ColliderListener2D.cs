using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ColliderListener2D : MonoBehaviour
{
    [SerializeField, ReadOnly] Collider2D[] _colliders2D;

    [Button("Update Colliders")]
    void UpdateColliderRefs()
    {
        _colliders2D = GetComponents<Collider2D>();
    }

#if UNITY_EDITOR
    void Reset() => UpdateColliderRefs();
#endif

    [SerializeField, Foldout("Events")] UnityEvent<Collider2D> _onTriggerEnter = new UnityEvent<Collider2D>();
    [SerializeField, Foldout("Events")] UnityEvent<Collider2D> _onTriggerStay = new UnityEvent<Collider2D>();
    [SerializeField, Foldout("Events")] UnityEvent<Collider2D> _onTriggerExit = new UnityEvent<Collider2D>();

    [SerializeField, Foldout("Events")] UnityEvent<Collision2D> _onCollisionEnter = new UnityEvent<Collision2D>();
    [SerializeField, Foldout("Events")] UnityEvent<Collision2D> _onCollisionStay = new UnityEvent<Collision2D>();
    [SerializeField, Foldout("Events")] UnityEvent<Collision2D> _onCollisionExit = new UnityEvent<Collision2D>();

    public event UnityAction<Collider2D> onTriggerEnter2D { add => _onTriggerEnter.AddListener(value); remove => _onTriggerEnter.RemoveListener(value); }
    public event UnityAction<Collider2D> onTriggerStay2D { add => _onTriggerStay.AddListener(value); remove => _onTriggerStay.RemoveListener(value); }
    public event UnityAction<Collider2D> onTriggerExit2D { add => _onTriggerExit.AddListener(value); remove => _onTriggerExit.RemoveListener(value); }

    public event UnityAction<Collision2D> onCollisionEnter2D { add => _onCollisionEnter.AddListener(value); remove => _onCollisionEnter.RemoveListener(value); }
    public event UnityAction<Collision2D> onCollisionStay2D { add => _onCollisionStay.AddListener(value); remove => _onCollisionStay.RemoveListener(value); }
    public event UnityAction<Collision2D> onCollisionExit2D { add => _onCollisionExit.AddListener(value); remove => _onCollisionExit.RemoveListener(value); }

    private void Awake()
    {
        _onTriggerEnter = new UnityEvent<Collider2D>();
        _onTriggerStay = new UnityEvent<Collider2D>();
        _onTriggerExit = new UnityEvent<Collider2D>();
        _onCollisionEnter = new UnityEvent<Collision2D>();
        _onCollisionStay = new UnityEvent<Collision2D>();
        _onCollisionExit = new UnityEvent<Collision2D>();
    }

    void OnTriggerEnter2D(Collider2D other) => _onTriggerEnter?.Invoke(other);
    void OnTriggerStay2D(Collider2D other) => _onTriggerStay?.Invoke(other);
    void OnTriggerExit2D(Collider2D other) => _onTriggerExit?.Invoke(other);
    void OnCollisionEnter2D(Collision2D collision) => _onCollisionEnter?.Invoke(collision);
    void OnCollisionExit2D(Collision2D collision) => _onCollisionExit?.Invoke(collision);
    void OnCollisionStay2D(Collision2D collision) => _onCollisionStay?.Invoke(collision);
}