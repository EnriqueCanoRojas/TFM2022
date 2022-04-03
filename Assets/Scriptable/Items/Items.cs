using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemClass
{
    Coin,
    PowerUp,
    Bomb,
    Key
}
[CreateAssetMenu]
public class Items : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemClass itemClass;

    [TextArea(15, 20)]
    public string Description = "";

    int itemID { get; set; }
    public int Raise;
    public int Deraise;

    public void OnBeforeSerialize()
    {
    }
    public void OnAfterDeserialize()
    {
    }
}