using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(UnitScript))]
public class AIController : MonoBehaviour
{

    private const float WANTED_MAX_DISTANCE = 2f;
    private const float TIME_ACTION_DURATION_FACTOR = 2f;
    private const float TIME_BETWEEN_ACTIONS = 6f;
    private const float SPEED_FACTOR = 1f;
    [SerializeField, BoxGroup("Physics2D")] ColliderListener2D _triggerZone;
    [SerializeField, BoxGroup("Physics2D"), ReadOnly] Rigidbody2D _rigidBody2D;
    [SerializeField, BoxGroup("Render")] Animator _animator;

    [SerializeField, Range(0f, 1f), BoxGroup("Setup")] float _stupidity = 0.5f;
    [SerializeField, Range(0f, 10f), BoxGroup("Setup")] float _speed = 1f;


    Coroutine _decisionRoutine = null;
    Coroutine _currentBehaviour = null;
    PlayerTag _target = null;
    Vector2 _moveVector;

    WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();



#if UNITY_EDITOR
    void Reset()
    {
        _rigidBody2D = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        _rigidBody2D = GetComponentInParent<Rigidbody2D>();
        if (Application.isPlaying)
        {
            Init();
        }
    }
#endif

    void Init()
    {
        WaitForSeconds delay = new WaitForSeconds(TIME_BETWEEN_ACTIONS * _stupidity);
        IEnumerator DecisionRoutine()
        {
            do
            {
                if (_target)
                {
                    if (Vector3.Distance(this.transform.position, _target.transform.position) > WANTED_MAX_DISTANCE)
                    {
                        MoveAction();
                    }
                    else
                    {
                        AttackAction();
                    }

                }
                yield return delay;
            } while (true);
        }

        if (_decisionRoutine != null)
            StopCoroutine(_decisionRoutine);
        _decisionRoutine = StartCoroutine(DecisionRoutine());
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _rigidBody2D = GetComponentInParent<Rigidbody2D>();
        Init();
        _triggerZone.onTriggerEnter2D += TryAcquiereTarget;
    }

    void TryAcquiereTarget(Collider2D col)
    {
        _target ??= col.GetComponentInParent<PlayerTag>();
    }

    bool ValidateDistance()
    {
        return Vector2.Distance(this.transform.position, _target.transform.position) < WANTED_MAX_DISTANCE;
    }

    void StopCurrentBehaviour()
    {
        if (_currentBehaviour == null)
            return;

        StopCoroutine(_currentBehaviour);
        _currentBehaviour = null;
    }

    void FaceTarget()
    {
        transform.rotation = _target?.transform.position.x < this.transform.position.x ?
        Quaternion.Euler(0f, -180f, 0f) :
        Quaternion.Euler(0f, 0f, 0f);
    }

    void MoveAction()
    {
        IEnumerator Coroutine()
        {
            do
            {
                if (_target)
                {
                    FaceTarget();
                    _rigidBody2D.MovePosition(_rigidBody2D.position + (Vector2)(_target.transform.position - transform.position).normalized * _speed * SPEED_FACTOR * Time.fixedDeltaTime);
                    _animator.SetBool("isWalking", true);
                }

                yield return _waitForFixedUpdate;
            } while (!ValidateDistance());
            StopCurrentBehaviour();
            _animator.SetBool("isWalking", false);
        }

        _currentBehaviour ??= StartCoroutine(Coroutine());
    }

    void AttackAction()
    {
        IEnumerator Coroutine()
        {
            Timer timer = new Timer().Start(3f);
            do
            {
                if (_target)
                {
                    FaceTarget();
                    _animator.SetTrigger("isAttacking");
                }
                yield return _waitForFixedUpdate;
            } while (!timer.eslaped);
            StopCurrentBehaviour();
        }

        _currentBehaviour ??= StartCoroutine(Coroutine());
    }


}
