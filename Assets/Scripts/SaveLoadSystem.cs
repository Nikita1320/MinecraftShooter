using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public bool[] LoadAccessWeapon()
    {
        //Load
        return playerData.accessWeapon;
    }
    public int[] LoadImprovementWeapon()
    {
        //Load
        return playerData.improvementWeapon;
    }
    public int[] LoadResourcesAmmount()
    {
        return playerData.ammountResources;
    }
    public int LoadGameLevel()
    {
        return playerData.gameLevel;
    }
    public int LoadImprovementHealth()
    {
        return playerData.levelHealthStat;
    }
    public int LoadImprovementSpeed()
    {
        return playerData.levelMoveSpeed;
    }
    public int LoadImprovementGrenade()
    {
        return playerData.levelGrenade;
    }

    //SaveMethods
    public void SaveAccessWeapon(bool[] accessWeapon)
    {
        //Load
        playerData.accessWeapon = accessWeapon;
    }
    public void SaveImprovementWeapon(int[] imrovementWeapon)
    {
        //Load
        playerData.improvementWeapon = imrovementWeapon;
    }
    public void SaveResourcesAmmount(int[] ammountResources)
    {
        playerData.ammountResources = ammountResources;
    }
    public void SaveGameLevel(int level)
    {
        playerData.gameLevel = level;
    }
    public void SaveImprovementHealth(int level)
    {
        playerData.levelHealthStat = level;
    }
    public void SaveImprovementSpeed(int level)
    {
        playerData.levelMoveSpeed = level;
    }
    public void SaveImprovementGrenade(int level)
    {
        playerData.levelGrenade = level;
    }
}
