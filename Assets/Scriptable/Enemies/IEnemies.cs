using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum EnemyClass
{
    Basic,
    MiniBoss,
    Boss,
    Special
}
[CreateAssetMenu]
public class IEnemies : ScriptableObject, ISerializationCallbackReceiver
{
    int ID { get; set; }
    [TextArea(15, 20)]
    public string Description = "";
    public int Experience;
    public int Power;
    public EnemyClass TypeEnemy;
    public int MaxEnemyHP;
    public GameObject[] drops;
    public bool isSpecialDrop;
    public GameObject specialDrop;

    //[System.NonSerialized]
    public int currentHP;

    void Awake()
    {
        currentHP = MaxEnemyHP;
    }
    void start()
    {
        ID = 0;
    }
    public void OnBeforeSerialize()
    {
       currentHP = MaxEnemyHP;
    }
    public void OnAfterDeserialize()
    {
        //currentHP = MaxEnemyHP;
    }
}