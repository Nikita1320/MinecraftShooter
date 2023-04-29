using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private List<Transform> spawnEnemyPoints = new();
    [SerializeField] private float rangeArea;

    public Transform PlayerPosition { get => playerPosition; }
    public List<Transform> SpawnEnemyPoints { get => spawnEnemyPoints; }
    public float RangeArea { get => rangeArea; }
}
