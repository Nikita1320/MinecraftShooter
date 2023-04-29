using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text ammountHP;

    private void Start()
    {
        health.changedHP += RenderHealth;
    }
    private void RenderHealth()
    {
        if (health.HP <= 0)
        {
            ammountHP.text = $"{0}/ {health.MaxHP}";
            slider.value = 0;
        }
        else
        {
            ammountHP.text = $"{health.HP}/ {health.MaxHP}";
            slider.value = health.HP / health.MaxHP;
        }
    }
    private void OnEnable()
    {
        RenderHealth();
    }
}
