using TMPro;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private HealthSystem target;
    [SerializeField] private TextMeshProUGUI dpsText;
    private int totalDamage;
    private float highestDps = new float();
    private float dps;

    private void Awake()
    {
        target.onDamage += AddTotalDamage;
        Timer.Create(transform, CountDamage, 10f, true);
        highestDps = PlayerPrefs.GetFloat("highestDps");
    }

    private void AddTotalDamage(int damage)
    {
        totalDamage += damage;
    }

    private void CountDamage()
    {
        dps = Mathf.Abs(totalDamage / 10);
        if (dps > highestDps)
        {
            highestDps = dps;
            PlayerPrefs.SetFloat("highestDps", highestDps);
        }

        dpsText.text = "DPS: " + dps;
        totalDamage = 0;

        if (dps > 200)
            dpsText.text = "Too much damage!\n Are you legit?";
    }
}
