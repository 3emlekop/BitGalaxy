using System.Collections;
using UnityEngine;

public class ShootAnimator : MonoBehaviour
{
    [SerializeField] private ParticleSystem shootParticle;
    private Transform turret;
    private Vector2 shootOffset = new Vector2(0, -0.15f);

    private void Awake()
    {
        turret = transform;
    }

    public void Shoot(Direction direction)
    {
        if(shootParticle != null)
            shootParticle.Play();

        turret.localPosition = shootOffset * (sbyte)direction;
        StartCoroutine(AnimateRecoil());
    }

    private IEnumerator AnimateRecoil()
    {
        for(byte i = 0; i < 10; i++)
        {
            turret.localPosition = Vector2.Lerp(turret.localPosition, Vector2.zero, 0.5f);
            yield return null;
        }
    }
}
