using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TypeResource
{
    Coin,
}
public class Bank : MonoBehaviour
{
    private static Bank instance;
    [SerializeField] private Resource[] resorces;
    [SerializeField] private SaveLoadSystem saveLoadSystem;
    public Action<TypeResource> resourceChangedAmmount;
    public static Bank Instance => instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        int[] ammount = saveLoadSystem.LoadResourcesAmmount();
        for (int i = 0; i < resorces.Length; i++)
        {
            if (i > ammount.Length)
            {
                resorces[i].ChangeAmmount(0);
            }
            else
            {
                resorces[i].ChangeAmmount(ammount[i]);
            }
        }

        for (int i = 0; i < resorces.Length; i++)
        {
            resorces[i].resourceChangedAmmount += SaveResources;
        }
    }
    public Resource GetResource(TypeResource typeResource)
    {
        foreach (var resource in resorces)
        {
            if (resource.TypeResource == typeResource)
            {
                return resource;
            }
        }
        return null;
    }
    private void SaveResources()
    {
        int[] ammount = new int[resorces.Length];
        for (int i = 0; i < ammount.Length; i++)
        {
            ammount[i] = resorces[i].Ammount;
        }
        saveLoadSystem.SaveResourcesAmmount(ammount);
    }
}
