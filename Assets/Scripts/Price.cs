using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Price
{
    [SerializeField] private TypeResource typeResource;
    [SerializeField] private int cost;

    public Price(TypeResource typeResource, int cost)
    {
        this.typeResource = typeResource;
        this.cost = cost;
    }
    public int Cost { get => cost;}
    public TypeResource TypeResource { get => typeResource; }
}
