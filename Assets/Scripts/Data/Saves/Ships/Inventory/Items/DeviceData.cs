using UnityEngine;

[System.Serializable]
public class DeviceData : ItemData
{
    [SerializeField] private float cooldown;
    [SerializeField] private int repairStrengthModifier;
    [SerializeField] private int startHealthModifier;
    [SerializeField] [Range(0, 100)] private int defenceModifier; 
    [SerializeField] [Range(0, 100)] private int evasionChanceModifier; 

    public float Cooldown => cooldown;
    public int RepairStrengthModifier => repairStrengthModifier;
    public int StartHealthModifier => startHealthModifier;
    public int DefenceModifier => defenceModifier;
    public int EvasionChanceModifier => evasionChanceModifier;
}