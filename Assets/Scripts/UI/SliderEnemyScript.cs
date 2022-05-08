using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderEnemyScript : MonoBehaviour
{
    public Slider slider;
    public Enemy enemy;
    public Image fill;
    public IEnemies thisEnemy;
    public GameObject FillArea;

    void Start()
    {
        this.gameObject.GetComponent<Slider>().minValue = 0;
        this.gameObject.GetComponent<Slider>().maxValue = thisEnemy.MaxEnemyHP;
    }
    void Update()
    {
        slider.value = enemy.MommentHP;
        fill.fillAmount = slider.value/slider.maxValue;
    }
}
