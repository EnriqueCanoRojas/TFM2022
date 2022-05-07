using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class WeaponCollision : MonoBehaviour
{
    public IWeapons weapon;
    public BoxCollider weaponCol;
    public Event attack;
    public Animator PlayerAnimator;


    void Start()
    {
        PlayerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        weaponCol = GetComponent<BoxCollider>();
        weaponCol.enabled = false;
    }
    void Update()
    {
        if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            weaponCol.enabled = true;
        }
        else
        {
            weaponCol.enabled = false;
        }
    }
}
