using UnityEngine;

public class ExplosionEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticlePrefab;
    [SerializeField] private ExplosionFlash flashPrefab;
    [SerializeField] private float flashScale;
    [SerializeField] private AudioClip explosionSound;

    private GameObject flash;
    private ParticleSystem smokeParticle;
    private Transform flashTransform;
    private Transform smokeParticleTransform;
    private Transform owner;
    private CameraShake cameraShake;
    private HealthSystem ownerHealthSystem;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        ownerHealthSystem = GetComponent<HealthSystem>();
        ownerHealthSystem.onDeath += Explode;

        flashPrefab.scale = flashScale;
        flash = Instantiate(flashPrefab.gameObject);
        flashTransform = flash.transform;

        smokeParticle = Instantiate(smokeParticlePrefab);
        smokeParticleTransform = smokeParticle.transform;

        owner = transform;
    }

    private void Explode(bool withLoot)
    {
        if (cameraShake != null)
        {
            cameraShake.Shake(0.2f, 0.05f);
            cameraShake.PlayExplosionSound(explosionSound, 1f);
        }

        if (smokeParticleTransform == null || flashTransform == null)
            return;

        smokeParticleTransform.position = owner.position;
        smokeParticle.Play();

        flashTransform.position = owner.position;
        flash.SetActive(true);
    }
}
