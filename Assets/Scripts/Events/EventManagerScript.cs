using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    public delegate void ExampleAction();
    public static event ExampleAction OnExample;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(OnExample != null)
                Debug.Log("Example was clicked");
                OnExample();
        }

        //Create the event in another script
        //void OnEnable()
        //{
        //EventManager.OnExample=+Examplecode;
        //}
        //void OnDisable()
        //{
        //EventManager.OnExample=-Examplecode;
        //}
        //void Examplecode()
        //{
        // Do Something
        //}
    }
}
