using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonT
{
    Start,
    Restart,
    Continue,
    Back,
    Option,
    Exit
}
[CreateAssetMenu(fileName = "Buttons", menuName = "ScriptableObjects/Buttons", order = 2)]
public class ButtonScriptable : ScriptableObject
{
    public string Name;
    public ButtonT button;
}