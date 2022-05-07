using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderEnemyScript : MonoBehaviour
{
    public IEnemies thisEnemy;
    public SliderScripts sliderScript;
    public GameObject FillArea;


    // Update is called once per frame
    void LateUpdate()
    {
        FillArea.GetComponent<Image>().fillAmount = this.gameObject.GetComponent<Slider>().value / 100;
        this.gameObject.GetComponent<Slider>().minValue = (int)thisEnemy.MaxEnemyHP;
        this.gameObject.GetComponent<Slider>().maxValue = (int)thisEnemy.MaxEnemyHP;
        this.gameObject.GetComponent<Slider>().value = (int)(thisEnemy.currentHP);
    }
}
