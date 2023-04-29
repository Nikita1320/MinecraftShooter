using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeWeapon
{
    Rifle,
    SubmachineGun,
    Shotgun,
    GrenadeLauncher
}
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string nameWeapon;
    [SerializeField] private Sprite weaponSprite;

    [SerializeField] private Price buyPrice;
    [SerializeField] private Price[] upgradePrice;
    [SerializeField] private int yandexBuyPrice;
    [SerializeField] private int[] yandexUpgradePrice;

    [SerializeField] private int baseDamage;
    [SerializeField] private int increseDamage;

    [SerializeField] private int baseClip;
    [SerializeField] private int increseClip;

    [SerializeField] private int baseRateOfFire;
    [SerializeField] private int increseRateOfFire;

    [SerializeField] private TypeWeapon typeWeapon;
    [SerializeField] private AnimatorOverrideController overrideController;
    public AnimatorOverrideController OverrideController { get => overrideController; }
    public Price BuyPrice { get => buyPrice; }
    public Sprite WeaponSprite { get => weaponSprite;}
    public int GetDamageValue(int level) => baseDamage + level * increseDamage;
    public int GetClipValue(int level) => baseClip + level * increseClip;
    public int GetRateOfFireValue(int level) => baseRateOfFire + level * increseRateOfFire;
    public int MaxLevel => 3;
    public TypeWeapon TypeWeapon { get => typeWeapon; }
    public string NameWeapon { get => nameWeapon;}
    public int[] YandexUpgradePrice { get => yandexUpgradePrice; }
    public int YandexBuyPrice { get => yandexBuyPrice; }

    public Price GetUpgradePrice(int level) => upgradePrice[level];
}
