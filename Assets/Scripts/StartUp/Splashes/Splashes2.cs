using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splashes2 : MonoBehaviour
{
    public float LimitUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if(this.gameObject.transform.position.y<LimitUp)
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z);
    }
    
}
