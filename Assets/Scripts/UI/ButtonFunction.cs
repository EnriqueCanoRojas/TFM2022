using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public ButtonScriptable Buttontype;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPress()
    {
        if (Buttontype.button == ButtonT.Start)
        {
            // GameManager.StartGame = true;
            SceneManager.LoadScene(Buttontype.Name);
        }
        if (Buttontype.button == ButtonT.Option)
        {
            SceneManager.LoadScene("Setting");
        }
        if (Buttontype.button == ButtonT.Continue)
        {
            //GameManager.Pause = false;
        }
        if (Buttontype.button == ButtonT.Back)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Buttontype.button == ButtonT.Restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //  gameManagerScript.GameSetUP();
            Time.timeScale = 1;
            //  gameManagerScript.Level = 0;
            //  gameManagerScript.Stopwatch.SetActive(true);
            // gameManagerScript.GameOverMenu.SetActive(false);
            //   GameManager.StartGame = true;
        }
        if (Buttontype.button == ButtonT.Exit)
        {
            Application.Quit();
        }
    }
}