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
    private Vector2 _moveDir;
    private float _moveSpeed;
    private bool _isSprinting;
    private bool _isAttacking;

    private void Reset()
    {
        _moveSpeed = 0;
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

    void Update()
    {

    }

    void FixedUpdate()
    {
        _moveDir = GetMoveDir();
        _moveSpeed = GetMoveSpeed();
        _rb.MovePosition(_rb.position + (_moveDir * _moveSpeed * Time.fixedDeltaTime));
    }

    void RunStarted(InputAction.CallbackContext cc)
    {
        _moveDir = cc.ReadValue<Vector2>();
        _anim.SetBool("isWalking", true);
    }

    void RunPerformed(InputAction.CallbackContext cc)
    {
        _moveDir = cc.ReadValue<Vector2>();
        _anim.SetBool("isWalking", true);
    }

    void RunCanceled(InputAction.CallbackContext cc)
    {
        _moveDir = Vector2.zero;
        _anim.SetBool("isWalking", false);
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
        _moveSpeed = GetMoveSpeed();
        _anim.SetBool("isSprinting", _moveDir.magnitude != 0 ? true : false);
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
