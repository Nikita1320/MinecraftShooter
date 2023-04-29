using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictinary<T,K>
{
    public T key;
    public K value;
    public SerializableDictinary(T key, K value)
    {
        this.key = key;
        this.value = value;
    }
}
