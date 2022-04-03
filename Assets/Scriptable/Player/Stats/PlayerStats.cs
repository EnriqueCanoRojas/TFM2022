using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject, ISerializationCallbackReceiver
{
    public float HP;
    public float MP;
    public float STM;
    public float STMCDValue;
    public float STMWaste;
    public float Speed;
    public float JumpForce;
    public float TurnSpeed;
    public float AtSpeed;
    public float dashSpeed;
    public float Gravity;
    public float Key;
    public float Bombs;
    public float Coins;

    [System.NonSerialized]
    public float rHP;
    [System.NonSerialized]
    public float rMP;
    [System.NonSerialized]
    public float rSTM;
    [System.NonSerialized]
    public float rSTMCDValue;
    [System.NonSerialized]
    public float rSTMWaste;
    [System.NonSerialized]
    public float rSTMCD;
    [System.NonSerialized]
    public float rSpeed;
    [System.NonSerialized]
    public float rJumpForce;
    [System.NonSerialized]
    public float rTurnSpeed;
    [System.NonSerialized]
    public float rAtSpeed;
    [System.NonSerialized]
    public float rdashSpeed;
    [System.NonSerialized]
    public float rGravity;
    [System.NonSerialized]
    public float rKey;
    [System.NonSerialized]
    public float rBombs;
    [System.NonSerialized]
    public float rCoins;

    public void OnAfterDeserialize()
    {
        rHP = HP;
        rMP = MP;
        rSTM = STM;
        rSTMCDValue = STMCDValue;
        rSTMWaste = STMWaste;
        rSpeed = Speed;
        rJumpForce = JumpForce;
        rTurnSpeed = TurnSpeed;
        rAtSpeed = AtSpeed;
        rdashSpeed = dashSpeed;
        rGravity = Gravity;
        rKey = Key;
        rBombs = Bombs;
        rCoins = Coins;
    }

    public void OnBeforeSerialize() { }
}