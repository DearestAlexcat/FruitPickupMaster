using Client;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConveyorGroup
{
    [field: SerializeField] public ConveyorView Conveyor { get; private set; }
    [field: SerializeField] public List<Unit> Units { get; private set; }
}
