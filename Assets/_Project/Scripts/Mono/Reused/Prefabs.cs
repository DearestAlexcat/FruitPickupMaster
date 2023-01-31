using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    [field: SerializeField] public PopUpText PlusOne { get; private set; }
    [field: SerializeField] public List<Fruit> fruitsPrefabs { get; private set; } = new List<Fruit>();
}
