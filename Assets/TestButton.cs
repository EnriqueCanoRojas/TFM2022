using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public ToggleVariable toogle;
    // Start is called before the first frame update

    public void Clicking()
    {
        if(toogle.RuntimeToogle==true)
        {
            OnClickFalse();
        }
        if(toogle.RuntimeToogle==false)
        {
            OnClickTrue();
        }
    }
    public void OnClickTrue()
    {
        toogle.RuntimeToogle = true;
    }
    public void OnClickFalse()
    {
        toogle.RuntimeToogle = false;
    }
}
