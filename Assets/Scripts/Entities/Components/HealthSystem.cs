using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
#region events
    public delegate void OnDeath(bool withLoot);
    public event OnDeath OnDeathEvent;

    public delegate void OnDamage(int gainedHealth);
    public event OnDamage OnDamageEvent;
#endregion

    [Header("Particles")]
    [SerializeField] private ParticleSystem deathParticlePrefab;

    [Header("Properties")]
    [SerializeField] private int startHealth;
    [Range(0, 100)] [SerializeField] private byte defence;
    [Range(0, 100)] [SerializeField] private byte evasionChance;

    [Header("Visuals")]
    [SerializeField] private TextMeshProUGUI damageTextPrefab;
    [SerializeField] private GameObject flashPrefab;

    [Header("Audio")]
    [SerializeField] private AudioSource hitAudioSource;

    private Transform canvas;
    public int CurrentHealth { get; private set; }
    public int StartHealth => startHealth;
    public byte Defence => defence;
    public byte Evasion => evasionChance;

    private TextMeshProUGUI damageText;
    private Vector3 textPosition = new Vector3();
    private ParticleSystem deathParticle;
    private Transform entity;
    private Transform deathParticleTransform;
    private GameObject flashObject;
    private DamageFlash flash;

    private readonly Color damageColor = new Color(0.98f, 0.98f, 0.98f);
    private readonly Color critColor = new Color(1, 0.6f, 0.6f);
    private readonly Color healColor = new Color(0.8f, 1, 0.6f);

    private void Awake()
    {
        CurrentHealth = startHealth;
        entity = transform;

        if (Debugger.instance != null)
            canvas = Debugger.instance.transform;

        deathParticle = Instantiate(deathParticlePrefab);
        deathParticleTransform = deathParticle.transform;

        flashObject = Instantiate(flashPrefab, entity);
        flashObject.GetComponent<SpriteRenderer>().sprite = entity.GetComponent<SpriteRenderer>().sprite;
        flash = flashObject.GetComponent<DamageFlash>();
    }

    public void AddStartHealth(int value)
    {
        if (startHealth + value > 0)
            startHealth += value;
        else
            startHealth = 1;

        CurrentHealth = startHealth;
    }

    public void AddDefence(float value)
    {
        defence = (byte)Mathf.Clamp(defence + value, 0, 100);
    }

    private void OnEnable()
    {
        CurrentHealth = startHealth;
    }

    public byte TakeDamage(int amount, bool isCrit)
    {
        if (amount == 0)
            return 0;

        amount = isCrit ? Mathf.CeilToInt(amount * 2.5f * (1 - defence)) : Mathf.CeilToInt(amount * (1 - defence));
        CurrentHealth -= amount;
        ValuePopUp(amount, critColor);
        OnDamageEvent?.Invoke(-amount);

        if (CurrentHealth <= 0)
            Die(true);
        else
        {
            if(hitAudioSource.enabled)
                hitAudioSource.Play();

            flashObject.SetActive(true);
            flash.Flash(Color.white);
        }
        return (byte)amount;
    }

    public void Die(bool withLoot)
    {
        if (gameObject.activeSelf == false)
            return;

        deathParticleTransform.position = entity.position;
        deathParticle.Play();
        gameObject.SetActive(false);

        if (ScorePointer.instance != null && withLoot)
            ScorePointer.instance.AddScore(startHealth / 5);

        OnDeathEvent?.Invoke(withLoot);
    }

    private void ValuePopUp(int damage, Color color)
    {
        textPosition.y = entity.position.y;
        textPosition.x = entity.position.x + Random.Range(-0.2f, 0.3f);
        damageText = Instantiate(damageTextPrefab, textPosition, Quaternion.identity, canvas);
        damageText.text = damage.ToString();
        damageText.color = color;
    }

    public void Heal(int value)
    {
        flash.Flash(healColor);
        if (CurrentHealth == startHealth)
            return;

        if (CurrentHealth + value > startHealth)
            CurrentHealth = startHealth;
        else
            CurrentHealth += value;

        ValuePopUp(value, healColor);
        OnDamageEvent?.Invoke(value);
    }
}
