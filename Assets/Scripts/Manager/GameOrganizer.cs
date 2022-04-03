using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameOrganizer : MonoBehaviour
{
    public static GameOrganizer instance = null;
    public GameManager gameManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // switch
        // Caso 1
        // Caso 2
        // Caso 3
        // Caso 4
    }

    // Update is called once per frame
    void Update()
    {
    }
}