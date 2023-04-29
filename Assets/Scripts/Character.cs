using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Price[] upgradeHealthPrice;
    [SerializeField] private Price[] upgradeSpeedPrice;
    [SerializeField] private Price[] upgradeGrenadePrice;

    [SerializeField] private int currentLevelHealth;
    [SerializeField] private int currentLevelSpeed;
    [SerializeField] private int currentLevelGrenade;

    [SerializeField] private int baseHealth;
    [SerializeField] private int baseAmmountGrenade;
    [SerializeField] private int baseSpeedValue;

    [SerializeField] private int increseValueHealth;
    [SerializeField] private int increseValueSpeed;
    [SerializeField] private int increseValueGrenade;
    [SerializeField] private SaveLoadSystem saveLoadSystem;
    public int BaseHealth { get => baseHealth; }
    public int BaseAmmountGrenade { get => baseAmmountGrenade; }
    public int BaseSpeedValue { get => baseSpeedValue; }
    public int CurrentLevelHealth { get => currentLevelHealth; }
    public int CurrentLevelSpeed { get => currentLevelSpeed; }
    public int CurrentLevelGrenade { get => currentLevelGrenade; }
    public int HealthPoint => baseHealth + increseValueHealth * currentLevelHealth;
    public int Speed => baseSpeedValue + increseValueSpeed * currentLevelSpeed;
    public int AmmountGrenade => baseAmmountGrenade + increseValueGrenade * currentLevelGrenade;

    public Price[] UpgradeHealthPrice { get => upgradeHealthPrice; }
    public Price[] UpgradeSpeedPrice { get => upgradeSpeedPrice; }
    public Price[] UpgradeGrenadePrice { get => upgradeGrenadePrice; }
    public Price CurrentUpgradeHealthPrice { get => upgradeHealthPrice[currentLevelHealth]; }
    public Price CurrentUpgradeSpeedPrice { get => upgradeSpeedPrice[currentLevelSpeed]; }
    public Price CurrentUpgradeGrenadePrice { get => upgradeGrenadePrice[currentLevelGrenade]; }

    public int MaxLevelHealth => upgradeHealthPrice.Length;
    public int MaxLevelSpeed => upgradeSpeedPrice.Length;
    public int MaxLevelGrenade => upgradeGrenadePrice.Length;

    private void Awake()
    {
        currentLevelHealth = saveLoadSystem.LoadImprovementHealth();
        currentLevelSpeed = saveLoadSystem.LoadImprovementSpeed();
        currentLevelGrenade = saveLoadSystem.LoadImprovementGrenade();
    }
    public void ImproveSpeed()
    {
        currentLevelSpeed++;
        saveLoadSystem.SaveImprovementSpeed(currentLevelSpeed);
    }
    public bool AdImproveSpeed()
    {
        //ad
        if (true)
        {
            ImproveSpeed();
            return true;
        }
        return false;
    }
    public void ImproveHealth()
    {
        currentLevelHealth++;
        saveLoadSystem.SaveImprovementHealth(currentLevelHealth);
    }
    public bool AdImproveHealth()
    {
        //ad
        if (true)
        {
            ImproveHealth();
            return true;
        }
        return false;
    }
    public void ImproveGrenade()
    {
        currentLevelGrenade++;
        saveLoadSystem.SaveImprovementGrenade(currentLevelGrenade);
    }
    public bool AdImproveGrenade()
    {
        //ad
        if (true)
        {
            ImproveGrenade();
            return true;
        }
        return false;
    }
}
