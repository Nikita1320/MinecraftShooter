using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Resource
{
    [SerializeField] private TypeResource typeResource;
    [SerializeField] private int ammount;
    [SerializeField] private Sprite iconResource;
    public Action resourceChangedAmmount;
    public int Ammount { get => ammount; }
    public TypeResource TypeResource { get => typeResource; }
    public Sprite IconResource { get => iconResource; }

    public bool ChangeAmmount(int value)
    {
        if (ammount + value < 0)
        {
            return false;
        }
        else
        {
            ammount += value;
            resourceChangedAmmount?.Invoke();
            return true;
        }
    }
}
