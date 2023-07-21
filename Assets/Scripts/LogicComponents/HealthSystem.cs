using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void OnDeath();
    public event OnDeath onDeath;

    public delegate void OnDamage(int damage);
    public event OnDamage onDamage;

    [SerializeField] private ParticleSystem deathParticlePrefab;
    [SerializeField] private int startHealth;
    [Range(0, 1)]
    [SerializeField] private float defence;
    [SerializeField] private TextMeshProUGUI damageTextPrefab;
    [SerializeField] private GameObject flashPrefab;

    public Transform canvas;
    private TextMeshProUGUI damageText;
    private Vector3 textOffset = new Vector3();

    private ParticleSystem deathParticle;
    private Transform entity;
    private Transform particle;

    private GameObject flashObject;
    private DamageFlash flash;

    private Color critColor = new Color(1, 0.6f, 0.6f, 1);
    private Color healColor = new Color(0.8f, 1, 0.6f, 1);
    public int health { get; private set; }

    private void Awake()
    {
        health = startHealth;
        entity = transform;

        if(Debugger.instance != null)
            canvas = Debugger.instance;

        deathParticle = Instantiate(deathParticlePrefab);
        particle = deathParticle.transform;

        flashObject = Instantiate(flashPrefab, entity);
        flashObject.GetComponent<SpriteRenderer>().sprite = entity.GetComponent<SpriteRenderer>().sprite;
        flash = flashObject.GetComponent<DamageFlash>();
        flashObject.SetActive(false);
    }

    public void AddStartHealth(int value)
    {
        if (startHealth + value > 0)
            startHealth += value;
        else
            startHealth = 1;
    }

    public void AddDefence(float value)
    {
        defence = Mathf.Clamp(defence + value, 0, 1);
    }

    public int GetDeffence()
    {
        return Mathf.RoundToInt(defence * 100);
    }

    public int GetStartHealth()
    {
        return startHealth;
    }

    private void OnEnable()
    {
        health = startHealth;
        flash.gameObject.SetActive(true);
    }

    public void TakeDamage(int amount, bool isCrit)
    {
        flash.Flash();

        if (amount == 0)
            return;

        amount = isCrit ? Mathf.CeilToInt(amount * 1.5f * (1 - defence)) : Mathf.CeilToInt(amount * (1 - defence));
        onDamage(amount);
        health -= amount;
        DamagePopUp(amount, isCrit);

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        if (gameObject.activeSelf == false)
            return;

        particle.position = entity.position;
        deathParticle.Play();
        gameObject.SetActive(false);

        ScorePointer.instance.AddScore(startHealth / 10);
        onDeath?.Invoke();
    }

    private void DamagePopUp(int damage, bool isCrit)
    {
        textOffset.y = entity.position.y;
        textOffset.x = entity.position.x + (Random.Range(-5, 6) / 10);
        damageText = Instantiate(damageTextPrefab, textOffset, Quaternion.identity, canvas);
        damageText.text = damage.ToString();
        if (isCrit)
            damageText.color = critColor;
    }

    private void HealPopUp(int value)
    {
        textOffset.y = entity.position.y;
        textOffset.x = entity.position.x + (Random.Range(-5, 6) / 10);
        damageText = Instantiate(damageTextPrefab, textOffset, Quaternion.identity, canvas);
        damageText.text = "+" + value.ToString();
        damageText.color = healColor;
    }

    public void Repair(byte value)
    {
        health += value;
        HealPopUp(value);
        flash.Flash(healColor);
    }
}
