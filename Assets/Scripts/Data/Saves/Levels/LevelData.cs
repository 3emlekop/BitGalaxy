using System;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType
{
    normal, resource, main
}

[Serializable]
public class LevelData
{
    [SerializeField] private LevelType levelType;
    [SerializeField] private int length;
    [SerializeField] private float flySpeed;
    [SerializeField] private List<Ship> LevelShips { get; set; }
}