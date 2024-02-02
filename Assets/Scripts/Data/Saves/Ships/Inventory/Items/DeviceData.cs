using System;
using UnityEngine;

[Serializable]
public class DeviceData : ItemData
{
    [SerializeField] private float cooldown;
    [SerializeField] private int repairStrengthModifier;
    [SerializeField] private int startHealthModifier;
    [SerializeField] [Range(0, 100)] private int defenceModifier; 
    [SerializeField] [Range(0, 100)] private int evasionModifier; 
    [SerializeField] private string abilityObjectName; 

    public float Cooldown => cooldown;
    public int RepairStrengthModifier => repairStrengthModifier;
    public int StartHealthModifier => startHealthModifier;
    public int DefenceModifier => defenceModifier;
    public int EvasionModifier => evasionModifier;
    new public ItemType ItemType => ItemType.Devices;
    public GameObject AbilityObject => ResourceManager.instance.GetAbilityObject(abilityObjectName);
}