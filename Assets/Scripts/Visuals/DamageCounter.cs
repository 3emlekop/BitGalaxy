using TMPro;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private HealthSystem target;
    [SerializeField] private TextMeshProUGUI dpsText;
    private int totalDamage;

    private void Awake()
    {
        target.onDamage += AddTotalDamage;
        Timer.Create(transform, CountDamage, 10f, true);
    }

    private void AddTotalDamage(int damage)
    {
        totalDamage += damage;
    }

    private void CountDamage()
    {
        dpsText.text = "DPS: " + ((float)totalDamage / 10).ToString();
        totalDamage = 0;
    }
}
