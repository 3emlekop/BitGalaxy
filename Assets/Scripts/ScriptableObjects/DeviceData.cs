using UnityEngine;
public enum SpecialAbility
{
    Shield, Repair, Attack, Mining, Teleport, Magnet, Assist, Barrier, Scanner
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Component")]
public class DeviceData : ItemData
{

    [Header("Modificators")]
    public byte health;

    [Range(0, 1f)]
    public float defence;

    public byte repairStrength;

    public SpecialAbility ability;

    public float cooldown = 1f;
}
