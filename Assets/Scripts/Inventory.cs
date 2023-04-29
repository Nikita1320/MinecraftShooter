using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private List<Weapon> rentWeapon = new();
    [SerializeField] private GameManager gameManager;

    [SerializeField] private SaveLoadSystem saveLoadSystem;
    private Dictionary<Weapon, bool> purchasedWeapon = new();
    private Dictionary<Weapon, bool> accessableWeapon = new();
    private Dictionary<Weapon, int> levelWeapon = new();
    public Dictionary<Weapon, bool> AccessableWeapon { get => accessableWeapon;}
    public Dictionary<Weapon, int> LevelWeapon { get => levelWeapon; }
    public Weapon[] Weapons { get => weapons; }

    private void Awake()
    {
        //Load
        bool[] access = saveLoadSystem.LoadAccessWeapon();
        int[] improvement = saveLoadSystem.LoadImprovementWeapon();
        for (int i = 0; i < weapons.Length; i++)
        {
            purchasedWeapon.Add(weapons[i], access[i]);
            levelWeapon.Add(weapons[i], improvement[i]);
        }
        gameManager.endedBattle += CloseRentWeapon;
        accessableWeapon = purchasedWeapon;
    }
    public void Improve(Weapon improvedWeapon)
    {
        levelWeapon[improvedWeapon]++;

        int[] improvement = new int[levelWeapon.Count];
        levelWeapon.Values.CopyTo(improvement, 0);
        saveLoadSystem.SaveImprovementWeapon(improvement);
    }
    public void OpenWeapon(Weapon weapon)
    {
        purchasedWeapon[weapon] = true;
        accessableWeapon[weapon] = true;

        bool[] access = new bool[purchasedWeapon.Count];
        purchasedWeapon.Values.CopyTo(access, 0);
        saveLoadSystem.SaveAccessWeapon(access);
    }
    public bool RentWeapon(Weapon weapon)
    {
        //ad
        if (true)
        {
            rentWeapon.Add(weapon);
            accessableWeapon[weapon] = true;
            return true;
        }
        return false;
    }
    public bool ImproveForAd(Weapon weapon)
    {
        //ad
        if (true)
        {
            Improve(weapon);
            return true;
        }
        return false;
    }
    public bool YandexImproveWeapon(Weapon weapon)
    {
        if (true)
        {
            Improve(weapon);
            return true;
        }
        return false;
    }
    public bool YandexBuyWeapon(Weapon weapon)
    {
        if (true)
        {
            OpenWeapon(weapon);
            return true;
        }
        return false;
    }
    public void CloseRentWeapon()
    {
        foreach (var weapon in rentWeapon)
        {
            accessableWeapon[weapon] = false;
        }
        rentWeapon.Clear();
    }
}
