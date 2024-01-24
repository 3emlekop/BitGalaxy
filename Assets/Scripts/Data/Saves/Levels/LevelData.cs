using System;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    normal, resource, main
}

[Serializable]
public class LevelData
{
    [SerializeField] private Type levelType;
    [SerializeField] private int length;
    [SerializeField] private float flySpeed;
    [SerializeField] private List<Ship> LevelShips { get; set; }
}