using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfFallsDestroy : MonoBehaviour
{
    void FixedUpdate()
    {
        if(this.gameObject.transform.position.y<-20)
        {
            Destroy(this.gameObject);
        }
    }
}