using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Player : MonoBehaviour
{
    //Basic Stats
    public PlayerStats playerStats;

    //Bools
    public ToggleVariable STMCD;
    public ToggleVariable hasUsableItem;
    public ToggleVariable Pause;
    public ToggleVariable GameOver;
    public ToggleVariable BeHit;
    public ToggleVariable isGrounded;

    private Vector3 moveDirection = Vector3.zero;

    //Events
    public EventTrigger BeingHit;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;

    public Animator _playerAnim;
    public Quaternion currentRotation;
    //inputs
    public float attack;
    public float dash;
    public Vector3 dashStrike;

    public Vector3 _input;
    //public Vector3 jinput;
    public string deviceClass { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = this.GetComponent<Rigidbody>();
        _model = this.gameObject.transform;
        _playerAnim = this.gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        currentRotation = this.gameObject.transform.rotation;
        GatherInput();
        // GatherSecondaryInput();
        Look();
        Attack();
        if (playerStats.rSTM > 0 && STMCD.RuntimeToogle)
        {
            Dash();
            // STM.RuntimeValue -= STMWaste.RuntimeValue;
            // STMCD.RuntimeToogle = false;
            // STMColdown(STMCDValue.RuntimeValue, STMCD.RuntimeToogle);
        }
        if (_rigidBody.velocity.magnitude != 0)
        {
            _playerAnim.SetFloat("Speed", 0.5f);
        }
        else if (_rigidBody.velocity.magnitude >= 1)
        {
            _playerAnim.SetFloat("Speed", 1f);
        }
        else
        {
            _playerAnim.SetFloat("Speed", 0f);
        }
    }
    private void FixedUpdate()
    {
        Move();
        //Attack();
        // Jump();
        if (playerStats.rSTM < 0)
        {
            playerStats.rSTM = 0;
        }
        if (playerStats.rSTM < playerStats.STM)
        {
            playerStats.rSTM += 1;
        }
    }
    public void Move()
    {
        //_rigidBody.MovePosition(transform.position + _input.ToIso() * _input.normalized.magnitude * Speed.RuntimeValue * Time.deltaTime);
        //Version con salto.

        _rigidBody.MovePosition(transform.position + new Vector3(_input.x * playerStats.rSpeed, _input.y * playerStats.rJumpForce, _input.z * playerStats.rSpeed).ToIso() * _input.normalized.magnitude * Time.deltaTime);
    }
    public void Attack()
    {
        //Init attack after input
        //Animation Controls
        if (attack != 0)
            _playerAnim.SetTrigger("Attack");
    }
    //Dash
    public void Dash()
    {
        _rigidBody.AddForce(transform.forward + dashStrike.ToIso() * playerStats.rdashSpeed * dash * dashStrike.normalized.magnitude * Time.deltaTime, ForceMode.Acceleration);
    }
    //looking
    public void Look()
    {
        if (_input == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rot, playerStats.rTurnSpeed * Time.deltaTime);
        if (_model.rotation.x != 0 || _model.rotation.z != 0)
        {
            _model.rotation = new Quaternion(0, _model.rotation.y, 0, _model.rotation.w);
        }
    }
    //Gather Basic inputs movement.
    public void GatherInput()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector3 stickValue = Gamepad.current.leftStick.ReadValue();
            float ButtonValueA = Gamepad.current.buttonSouth.ReadValue();

            //Animation Controls
            if (ButtonValueA != 0 && !isGrounded.RuntimeToogle)
                _playerAnim.SetTrigger("Jump");

            dash = Gamepad.current.rightShoulder.ReadValue();
            attack = Gamepad.current.leftShoulder.ReadValue();

            // _inputj = new Vector3(0, ButtonValueA, 0);
            _input = new Vector3(stickValue.x, ButtonValueA, stickValue.y);
        }
        else
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Jump"), Input.GetAxisRaw("Vertical"));

            //Animation Controls
            if (Input.GetAxis("Jump") != 0 && isGrounded.RuntimeToogle!)
                _playerAnim.SetTrigger("Jump");

            dash = Keyboard.current.FindKeyOnCurrentKeyboardLayout("f").ReadValue();
            attack = Keyboard.current.FindKeyOnCurrentKeyboardLayout("e").ReadValue();
        }
    }
    //Gather non basic inputs, Glide,....
    public void GatherSecondaryInput()
    {
    }
    public IEnumerator STMColdown(float CD, bool Return)
    {
        yield return new WaitForSeconds(CD);
        Return = true;
    }
    public void Hitted(float dmg)
    {
        playerStats.rHP -= dmg;
    }
    // public void PlayerMoveSet()
    //  {
    //     if (Input.GetKeyDown(KeyCode.Tab) && STM.RuntimeValue > 0 && STMCD.RuntimeToogle == false)
    //     {
    //Dash
    //         STMCD.RuntimeToogle = true;
    //         STMColdown(STMCDValue.RuntimeValue, STMCD.RuntimeToogle);
    //     }
    //public IEnumerator PosionCo()
    //{
    //   int temp = 0;
    //   while (temp < numberOfPoison)
    //   {
    //       Health.Instance.health -= posionDmg;
    //       playerSprite.color = poisonColor;
    //       yield return new WaitForSeconds(poisonDuration);
    //       playerSprite.color = normalColor;
    //       yield return new WaitForSeconds(poisonDuration);
    //       temp++;
    //  }

    public IEnumerator BeHittedCD(float CD, bool done, float dmg)
    {
        if (!done)
        {
            yield return new WaitForSecondsRealtime(CD);
            Hitted(dmg);
            done = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded.RuntimeToogle = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded.RuntimeToogle = false;
        }
    }
}
public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}