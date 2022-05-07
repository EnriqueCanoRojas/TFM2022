using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance2 = null;

    //PlayerStats Shared with the gameManager
    public PlayerStats playerStats;

    //Public conditions
    public ToggleVariable GameOver;
    public ToggleVariable Pause;
    public ToggleVariable StartGame;

    //
    public GameObject DungueonGenerator;
    //
    public IntVariable Seed;
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
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            GameOver.RuntimeToogle = false;
            Pause.RuntimeToogle = false;
        }
        //Scenes excluded the MainMenu
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            GameStartControl();
        }
    }
    public void GameStartControl()
    {
        StartGame.RuntimeToogle = true;
        DungueonGenerator.SetActive(true);
        PauseProtocol();

        if (Input.GetKeyDown(KeyCode.P) && Pause.RuntimeToogle == false)
            Pause.RuntimeToogle = true;
        else if (Input.GetKeyDown(KeyCode.P))
            Pause.RuntimeToogle = false;

        if (playerStats.rHP <= 0)
        {
            GameOver.RuntimeToogle = true;
        }
        if (GameOver.RuntimeToogle == true)
        {
            GameOverProtocol();
        }
    }
    public void PauseProtocol()
    {
        if (StartGame.RuntimeToogle==true)
        {
            switch (Pause.RuntimeToogle)
            {
                case true:
                    Time.timeScale = 0;
                    PausePanel.SetActive(true);
                    break;
                case false:
                    Time.timeScale = 1;
                    PausePanel.SetActive(false);
                    break;

            }
        }
    }
    void GameOverProtocol()
    {
        switch (GameOver.RuntimeToogle)
        {
            case true:
                Time.timeScale = 0;
                GameOverPanel.SetActive(true);
                break;
            case false:
                Time.timeScale = 1;
                GameOverPanel.SetActive(false);
                break;

        }
    }
}