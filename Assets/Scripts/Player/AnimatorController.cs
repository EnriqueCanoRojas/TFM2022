using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorController : MonoBehaviour
{
    public Animator _playerAnim;
    public Transform _model;
    public float speed;
    public Vector3 currentPos;
    public Vector3 oldPosition;
    float timer = 0;

    //Non direct Animations are called here, like movement, reactions.
    void Start()
    {
        currentPos = new Vector3(0, 0, 0);
        oldPosition = new Vector3(0, 0, 0);
        _playerAnim = this.gameObject.GetComponent<Animator>();
        _model = this.gameObject.transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        currentPos = _model.position;
        if (timer > 1f)
        {
            oldPosition = _model.position;
            timer = 0;
        }
        speed = Vector3.Distance(currentPos, oldPosition);
        var speedPerSec = Vector3.Distance(currentPos, oldPosition) / Time.deltaTime;
        AnimationControls(speed);
    }
    public void AnimationControls(float speed)
    {
        if (speed >= 1f)
            _playerAnim.SetFloat("Speed", 1f);
        if (speed < 1f)
        {
            _playerAnim.SetFloat("Speed", 0f);
        }
    }

}
