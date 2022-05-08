using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public PlayerStats playerStats;

    public Slider sliderHP;
    public Image fillHP;

    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        sliderHP.minValue = 0;
        sliderHP.maxValue = playerStats.HP;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins: " + playerStats.rCoins;
        sliderHP.value = playerStats.rHP;
        fillHP.fillAmount = sliderHP.value / sliderHP.maxValue;
    }
}
