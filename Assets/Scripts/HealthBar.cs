using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        SetHealth(health);
    }

    // Update is called once per frame
    public void SetHealth(float health)
    {
        slider.value = health;
        healthText.SetText("Health: " +health+'/'+slider.maxValue);
    }
}
