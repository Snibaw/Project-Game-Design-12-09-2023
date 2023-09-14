using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider energieSlider;
    public void InitHealthSlider(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }
    public void SetHealthSliderValue(float health)
    {
        healthSlider.value = health;
    }
    public void InitEnergieSlider(float maxEnergie)
    {
        energieSlider.maxValue = maxEnergie;
    }
    public void SetEnergieSliderValue(float energie)
    {
        energieSlider.value = energie;
    }
}
