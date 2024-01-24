using System.Collections;
using UnityEngine;

public class ShootAnimator : MonoBehaviour
{
    [SerializeField] private ParticleSystem shootParticle;
    
    private Transform turretTransform;
    private Vector2 shootOffset = Vector2.zero;
    private Vector2 startPos;

    private void Awake()
    {
        turretTransform = transform;
        startPos = transform.localPosition;
    }

    public void Animate(Vector2 direction)
    {
        if(shootParticle != null)
            shootParticle.Play();

        shootOffset = -direction / 5;
        turretTransform.localPosition = shootOffset;
        StopCoroutine(AnimateRecoil());
        StartCoroutine(AnimateRecoil());
    }

    private IEnumerator AnimateRecoil()
    {
        for(byte i = 0; i < 15; i++)
        {
            turretTransform.localPosition = Vector2.Lerp(turretTransform.localPosition, startPos, 0.3f);
            yield return null;
        }
    }
}
