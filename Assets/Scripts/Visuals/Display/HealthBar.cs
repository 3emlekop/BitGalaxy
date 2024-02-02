using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider slider;
    public HealthSystem healthSystem;

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnDamageEvent += UpdateBar;
        healthSystem.OnDeathEvent += ResetBar;
        slider.value = 1;

        maxHealthText.text = '/' + healthSystem.StartHealth.ToString();
        healthText.text = healthSystem.StartHealth.ToString();
    }

    private void UpdateBar(int damage)
    {
        healthText.text = healthSystem.CurrentHealth.ToString();
        slider.value = healthSystem.CurrentHealth / (float)healthSystem.StartHealth;
    }

    private void ResetBar(bool withLoot)
    {
        if (slider == null)
            return;

        slider.value = 1;
        healthText.text = healthSystem.StartHealth.ToString();
        slider.gameObject.SetActive(false);

        healthSystem.OnDamageEvent -= UpdateBar;
        healthSystem.OnDeathEvent -= ResetBar;
    }
}
