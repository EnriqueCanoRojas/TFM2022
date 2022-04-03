using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance2 = null;

    //PlayerStats Shared with the gameManager
    public FloatVariable PlayerHP;
    public FloatVariable MP;
    public FloatVariable STM;
    public FloatVariable Speed;
    public FloatVariable AtSpeed;
    public FloatVariable JumpForce;
    public FloatVariable Key;
    public FloatVariable Bombs;
    public ToggleVariable hasUsableItem;
    public ToggleVariable Pause;
    public ToggleVariable GameOver;

    //
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    void Awake()
    {
        if (instance2 == null)
        {
            instance2 = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance2 != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP.RuntimeValue <= 0)
        {
            GameOver.RuntimeToogle = true;
        }
        if (GameOver.RuntimeToogle == true)
        {
            GameOverProtocol();
        }
    }
    void PauseProtocol()
    {
        if (Pause.RuntimeToogle == true)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
    }
    void GameOverProtocol()
    {
    }
}