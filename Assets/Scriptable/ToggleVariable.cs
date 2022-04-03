using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToggleVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public bool InitialToogle;

    [System.NonSerialized]
    public bool RuntimeToogle;

    public void OnAfterDeserialize()
    {
        RuntimeToogle = InitialToogle;
    }

    public void OnBeforeSerialize() { }
}