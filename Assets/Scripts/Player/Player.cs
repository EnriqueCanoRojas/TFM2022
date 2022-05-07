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

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;

    [HideInInspector]
    public Quaternion currentRotation;


    //Direct Animations 
    public Animator _playerAnim;

    //inputs
    public float attack;
    public float dash;
    public Vector3 dashStrike;

    public Vector3 _input;
    public string deviceClass { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = this.gameObject.GetComponent<Animator>();
        _rigidBody = this.GetComponent<Rigidbody>();
        _model = this.gameObject.transform;
    }
    void Update()
    {
        currentRotation = this.gameObject.transform.rotation;
        GatherInput();
        // GatherSecondaryInput();
        Look();
        Attack();
    }
    private void FixedUpdate()
    {
        Move();
        Dash();
    }
    public void Move()
    {
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
        if ((dash > 0 || dash < 0) && playerStats.rSTM > 0)
        {
            if (STMCD.RuntimeToogle == true)
            {
                _rigidBody.AddForce(transform.forward + dashStrike.ToIso() * playerStats.rdashSpeed * dash * dashStrike.normalized.magnitude * Time.deltaTime, ForceMode.Impulse);
                playerStats.rSTM -= playerStats.rSTMWaste;
                STMColdown(playerStats.rSTMCDValue, STMCD.RuntimeToogle);
                STMCD.RuntimeToogle = false;
            }
        }
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
            dash = Gamepad.current.rightShoulder.ReadValue();
            attack = Gamepad.current.leftShoulder.ReadValue();
            _input = new Vector3(stickValue.x, ButtonValueA, stickValue.y);

            //Animation Controls
            if (ButtonValueA != 0 && isGrounded.RuntimeToogle)
            { 
                _playerAnim.SetTrigger("Jump");
            }
        }
        else
        {
            //Movements
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Jump"), Input.GetAxisRaw("Vertical"));
            dash = Keyboard.current.FindKeyOnCurrentKeyboardLayout("f").ReadValue();
            attack = Keyboard.current.FindKeyOnCurrentKeyboardLayout("e").ReadValue();


            //Animation Controls
            if (Input.GetAxis("Jump") != 0 && isGrounded.RuntimeToogle)
                _playerAnim.SetTrigger("Jump");
        }
    }
    public void Hitted(float dmg)
    {
        playerStats.rHP -= dmg;
    }
    public void PlayerMoveSet()
    {
        //Dash
        if (playerStats.rSTM > 0 && STMCD.RuntimeToogle)
        {

            // STM.RuntimeValue -= STMWaste.RuntimeValue;
            // STMCD.RuntimeToogle = false;
            // STMColdown(STMCDValue.RuntimeValue, STMCD.RuntimeToogle);
        }
        if (playerStats.rSTM < 0)
        {
            playerStats.rSTM = 0;
        }
        if (playerStats.rSTM < playerStats.STM)
        {
            playerStats.rSTM += 1;
        }
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
    }
    public IEnumerator STMColdown(float CD, bool Return)
    {
        yield return new WaitForSeconds(CD);
        Return = true;
    }
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