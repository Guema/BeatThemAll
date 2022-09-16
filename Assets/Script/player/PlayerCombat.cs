using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] InputActionReference _attackAction;
    [SerializeField,Range(0,10)] int _attackDamage;
    [SerializeField,Range(0f,3f)] float _attackDamageDelay;
    [SerializeField,Range(0f,10f)] float _attackSlowTime;
    PlayerMove _pmoveScript;
    HealthScript _healthScript;
    PlayerAttackZone _attackZoneScript;

    private void Awake()
    {
        _attackSlowTime = 0.4f;

        _pmoveScript = GetComponent<PlayerMove>();
        _healthScript = GetComponent<HealthScript>();
        _attackZoneScript = GetComponentInChildren<PlayerAttackZone>();
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
        _pmoveScript.SetActiveAttackSpeed(true);
        StartCoroutine(AttackSlowEffect());
        StartCoroutine(AttackDamageDelayed(_attackDamage, _attackDamageDelay, _attackZoneScript.GetTargets()));
    }

    IEnumerator AttackSlowEffect()
    {
        yield return new WaitForSeconds(_attackSlowTime);
        _pmoveScript.SetActiveAttackSpeed(false);
    }

    IEnumerator AttackDamageDelayed(int damage, float delay, List<HealthScript> targets)
    {
        yield return new WaitForSeconds(delay);
        
        foreach (HealthScript t in targets)
        {
            t.DealDamage(damage);
        }
    }
}
