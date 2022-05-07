using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfWeapon
{
    Maze,
    Sword,
    Shield
}
[CreateAssetMenu]
public class IWeapons : ScriptableObject, ISerializationCallbackReceiver
{
    int ID { get; set; }
    public string Name;
    [TextArea(15, 20)]
    public string Description = "";
    public int Power;
    public typeOfWeapon weapon;
    public int Price;

    [System.NonSerialized]
    public int PowerR;

    void start()
    {
        ID = 0;
    }
    public void OnBeforeSerialize()
    {
        PowerR = Power;
    }
    public void OnAfterDeserialize()
    {
    }
}
