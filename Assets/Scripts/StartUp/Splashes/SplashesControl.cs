using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashesControl : MonoBehaviour
{
    public int Time1;
    public int Time2;
    public int Time3;
    public int Time4;
    public int Time5;
    public int Time6;
    public int Time7;

    public Text splashText;
    public Color textColor;

    public GameObject title;
    public Color titleColor;

    public GameObject Viking;
    public Color VikingColor;

    public Text PersonalText;
    public Color PersonalTextColor;


    // public GameObject Controls;


    public bool VDone;
    public bool V1Done;
    public bool V2Done;
    public bool V3Done;
    public bool V4Done;
    public bool V5Done;
    public bool V6Done;
    public bool V7Done;

    // Start is called before the first frame update
    void Start()
    {
        PersonalTextColor = PersonalText.color;
        PersonalTextColor.a = 0.0f;

        Viking.SetActive(false);
        VikingColor =Viking.GetComponent<SpriteRenderer>().color;
        
        textColor = splashText.color;
        textColor.a = 0.0f;
        titleColor =title.GetComponent<SpriteRenderer>().color;
        titleColor.a = 0.0f;

        StartCoroutine(CDSplashStart1(Time1));
    }

    // Update is called once per frame
    void Update()
    {
        PersonalText.color = PersonalTextColor;
        Viking.GetComponent<SpriteRenderer>().color = VikingColor;
        splashText.color = textColor;
        title.GetComponent<SpriteRenderer>().color = titleColor;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Viking.transform.position.y >= 4)
        {
            VDone = true;
        }
        if (VDone)
        {
            //if(!T1Done)
            //{
                StartCoroutine(CDSplashFinish1(Time2));
                if (VikingColor.a <= 0f)
                {
                    //T1Done = true;
                    V1Done = true;
                }
            //}
        }
        if(V1Done)
        {
            StopCoroutine(CDSplashFinish1(0));
            //if (!T2Done)
           // {
                StartCoroutine(CDSplashStart2(Time3));
                if (PersonalTextColor.a >= 1f)
                {
                   // T2Done = true;
                    V2Done = true;
                }
            //}
        }
        if(V2Done)
        {
            StopCoroutine(CDSplashStart2(0));
            //if (!T3Done)
           // {
                StartCoroutine(CDSplashFinish2(Time4));
                if (PersonalTextColor.a >= 0.0f)
                {
                   // T3Done = true;
                    V3Done = true;
                }

           // }
        }
        if (V3Done)
        {
            StopCoroutine(CDSplashFinish2(0));
          //  if (!T4Done)
          //  {
                StartCoroutine(CDSplashStart3(Time5));
                if (textColor.a >= 1f)
                {
                   // T4Done = true;
                    V4Done = true;

                }
            //}
        }
        if (V4Done)
        {
            StopCoroutine(CDSplashStart3(0));
           // if (!T5Done)
            //{
                StartCoroutine(CDSplashFinish3(Time6));
                if (textColor.a >= 0f)
                {
                    V5Done = true;
                   // T5Done = true;
                }

           // }
        }
        if (V5Done)
        {
            StopCoroutine(CDSplashFinish3(0));
            //if (!T6Done)
           // {
                StartCoroutine(CDSplashStart4(Time7));
                if (textColor.a >= 1f)
                {
                  //  T6Done = true;
                    V6Done = true;

                }
          //  }
        }

    }
    void StartSecuence()
    {
        if (PersonalTextColor.a > 0)
            PersonalTextColor.a -= 0.001f;
        if (textColor.a > 0)
            textColor.a -= 0.001f;
        if (titleColor.a > 0)
            titleColor.a -= 0.01f;
        else if(PersonalTextColor.a==0 && textColor.a==0 && titleColor.a==0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    //Viking
    IEnumerator CDSplashStart1(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (Viking.activeSelf == false)
            Viking.SetActive(true);
    }

    IEnumerator CDSplashFinish1(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if(VikingColor.a>0)
            VikingColor.a -= 0.001f;
    }
    //Creators

    IEnumerator CDSplashStart2(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (PersonalTextColor.a < 100)
            PersonalTextColor.a += 0.001f;
    }
    IEnumerator CDSplashFinish2(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (PersonalTextColor.a > 0)
            PersonalTextColor.a -= 0.001f;
    }
    //Explanation

    IEnumerator CDSplashStart3(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (textColor.a < 100)
            textColor.a += 0.001f;
    }
    IEnumerator CDSplashFinish3(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (textColor.a > 0)
            textColor.a -= 0.001f;

    }

    IEnumerator CDSplashStart4(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (titleColor.a < 100)
            titleColor.a += 0.001f;
    }
    IEnumerator CDSplashFinish4(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (titleColor.a > 0)
            titleColor.a -= 0.01f;

    }
}
