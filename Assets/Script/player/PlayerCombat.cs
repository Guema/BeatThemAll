using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] InputActionReference _attackAction;
    [SerializeField,Range(0f,10f)] float _attackSlowTime;
    PlayerMove pmoveScript;

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
        _anim.SetTrigger("attack");
        pmoveScript.SetActiveAttackSpeed(true);
        StartCoroutine(AttackSlowEffect());
    }

    IEnumerator AttackSlowEffect()
    {
        float cd = 0;
        while (cd < _attackSlowTime)
        {
            cd += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        pmoveScript.SetActiveAttackSpeed(false);
    }
}
