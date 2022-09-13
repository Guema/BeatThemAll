using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerMove : MonoBehaviour
{
    [SerializeField] InputActionReference _runAction;
    [SerializeField] InputActionReference _sprintAction;
    [SerializeField] InputActionReference _jumpAction;
    [SerializeField] Animator _anim;
    [SerializeField, Range(0f, 10f)] float _runSpeed;
    [SerializeField, Range(0f, 10f)] float _sprintSpeed;
    [SerializeField, Range(0f, 10f)] float _attackMoveSpeed;
    [SerializeField, Range(0f, 2f)] float _jumpCooldown;
    private Rigidbody2D _rb;
    private bool _isSprinting;
    private bool _isAttacking;

    private void Reset()
    {
        _runSpeed = 4f;
        _sprintSpeed = 8f;
        _isSprinting = false;
        _isAttacking = false;
        _jumpCooldown = .6f;
        _attackMoveSpeed = 1f;
    }

    void Start()
    {
        _runAction.action.started += RunStarted;
        _runAction.action.performed += RunPerformed;
        _runAction.action.canceled += RunCanceled;

        _sprintAction.action.started += SprintStarted;
        _sprintAction.action.performed += SprintPerformed;
        _sprintAction.action.canceled += SprintCanceled;

        _jumpAction.action.started += JumpingStarted;

        _rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        _runAction.action.started -= RunStarted;
        _runAction.action.performed -= RunPerformed;
        _runAction.action.canceled -= RunCanceled;

        _sprintAction.action.started -= SprintStarted;
        _sprintAction.action.performed -= SprintPerformed;
        _sprintAction.action.canceled -= SprintCanceled;

        _jumpAction.action.started -= JumpingStarted;
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (GetMoveDir() * GetMoveSpeed() * Time.fixedDeltaTime));
    }

    void RunStarted(InputAction.CallbackContext cc)
    {
        if (cc.ReadValue<Vector2>().magnitude > 0.1f)
        {
            _anim.SetBool("isWalking", true);
            _anim.SetBool("isSprinting", _isSprinting ? true : false);
        }

        if (cc.ReadValue<Vector2>().x > 0.1f || cc.ReadValue<Vector2>().x < -0.1f)
        {
            transform.localScale = new Vector3(cc.ReadValue<Vector2>().x > 0.1f ? 1f : -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void RunPerformed(InputAction.CallbackContext cc)
    {
        if (cc.ReadValue<Vector2>().magnitude > 0.1f)
        {
            _anim.SetBool("isWalking", true);
            _anim.SetBool("isSprinting", _isSprinting ? true : false);
        }

        if (cc.ReadValue<Vector2>().x > 0.1f || cc.ReadValue<Vector2>().x < -0.1f)
        {
            transform.localScale = new Vector3(cc.ReadValue<Vector2>().x > 0.1f ? 1f : -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void RunCanceled(InputAction.CallbackContext cc)
    {
        _anim.SetBool("isWalking", false);
        _anim.SetBool("isSprinting", false);
        _isSprinting = false;
    }

    void SprintStarted(InputAction.CallbackContext cc)
    {
        _isSprinting = true;
        _anim.SetBool("isSprinting", true);
    }

    void SprintPerformed(InputAction.CallbackContext cc)
    {
        _isSprinting = true;
        _anim.SetBool("isSprinting", true);
    }

    void SprintCanceled(InputAction.CallbackContext cc)
    {
        _isSprinting = false;
        _anim.SetBool("isSprinting", false);
    }

    void JumpingStarted(InputAction.CallbackContext cc)
    {
        _anim.SetTrigger("Jump");
        StartCoroutine(JumpCancel());
    }

    IEnumerator JumpCancel()
    {
        float cd = 0;
        while (cd < _jumpCooldown)
        {
            cd += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        // update
        _anim.SetBool("isSprinting", GetMoveDir().magnitude != 0 ? true : false);
    }

    float GetMoveSpeed()
    {
        if (_isAttacking)
        {
            return _attackMoveSpeed;
        }
        else if (_isSprinting)
        {
            return _sprintSpeed;
        }
        else if (!_isSprinting)
        {
            return _runSpeed;
        }

        return 0;
    }

    public void SetActiveAttackSpeed(bool forceTrue = true)
    {
        _isAttacking = forceTrue;
    }

    Vector2 GetMoveDir()
    {
        return _runAction.action.ReadValue<Vector2>().magnitude > 0.1f ? _runAction.action.ReadValue<Vector2>() : Vector2.zero;
    }

}
