using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using NaughtyAttributes;
using System.Linq;
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] InputActionReference _attackAction;
    [SerializeField, Range(0f, 10f)] float _attackSlowTime;

    [SerializeField, BoxGroup] Capsule2D _attackZone = new Capsule2D();



    PlayerMove pmoveScript;

#if UNITY_EDITOR

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Vector2 temp = (Vector2)transform.position + _attackZone.offset;
        Vector2 a = temp + _attackZone.size / 2;
        Vector2 b = temp - _attackZone.size / 2;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(a, b);
    }

#endif



    private void Awake()
    {
        _attackSlowTime = 0.4f;

        pmoveScript = GetComponent<PlayerMove>();
    }

    void Start()
    {
        _attackAction.action.started += AttackStarted;
    }

    void OnDestroy()
    {
        _attackAction.action.started -= AttackStarted;
    }

    void AttackStarted(InputAction.CallbackContext cc)
    {
        IEnumerator AttackSlowEffect()
        {
            _anim.SetTrigger("attack");
            pmoveScript.SetActiveAttackSpeed(true);
            float cd = 0;
            while (cd < _attackSlowTime)
            {
                cd += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            pmoveScript.SetActiveAttackSpeed(false);

            // var hitEnnemies = Physics2D.OverlapCapsuleAll((Vector2)transform.position + _attackZone.offset, _attackZone.size, _attackZone.direction, 0f)

        }


        StartCoroutine(AttackSlowEffect());
    }


}

