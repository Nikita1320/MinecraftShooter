using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] ragdollObjects;

    public void Activate()
    {
        foreach (var rb in ragdollObjects)
        {
            rb.isKinematic = false;
        }
    }
}
