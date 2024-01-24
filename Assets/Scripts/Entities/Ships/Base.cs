using UnityEngine;

public class Base : Ship
{
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem.onDamage += CheckStage;
        SetTurretsEnabled(false);
    }

    /// <summary>
    /// Checks current boss health and returns number of turrets to enable.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    private void CheckStage(int damage)
    {
        if (healthSystem.CurrentHealth < healthSystem.StartHealth * 0.8f && healthSystem.CurrentHealth > healthSystem.StartHealth * 0.4f)
            Turrets[2].IsActive = true;

        if(healthSystem.CurrentHealth <= healthSystem.StartHealth * 0.4f)
            SetTurretsEnabled(true);
    }
}
