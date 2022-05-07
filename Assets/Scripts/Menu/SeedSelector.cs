using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelector : MonoBehaviour
{
    public InputField playerInput;
    public int userInput;
    public int lengthInput;
    public string InputPlayer;
    public bool chose;
    public static bool ready;

    public IntVariable Seed;

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }
    void ReadInput()
    {
        InputPlayer = playerInput.text;
        if (InputPlayer!="")
        {
            var isNumeric = int.TryParse(InputPlayer, out _);
            if (isNumeric)
            {
                userInput = Convert.ToInt32(InputPlayer);
                lengthInput = IntLength(userInput);
                if (lengthInput == 7)
                {
                    userInput = Convert.ToInt32(InputPlayer);
                    chose = true;
                    //StartProcess of selection Numbers
                }
                else
                {
                    chose = false;
                }
            }
            if(!chose)
                SelectRandom(isNumeric);
        }
        Seed.RuntimeValue = userInput;
    }
    void SelectRandom(bool isNumeric)
    {
        if ((InputPlayer == "") || !isNumeric || IntLength(userInput) != 7)
        {
            userInput = Convert.ToInt32(UnityEngine.Random.Range(1000000, 9999999));
            chose = true;
        }
    }

    public static int IntLength(int i)
    {
        if (i < 0)
            throw new ArgumentOutOfRangeException();

        if (i == 0)
            return 1;

        return (int)Math.Floor(Math.Log10(i)) + 1;
    }
}
