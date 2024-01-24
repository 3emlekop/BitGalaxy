using UnityEngine;

public class AutoTargeter : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float autoTargetForce;

    private Transform shipsParent;
    private Transform target;
    private Transform projectileTransform;
    private string ignoreTag;

    private void Awake()
    {
        projectileTransform = projectile.transform;
        ignoreTag = projectile.IgnoreTag;
        shipsParent = projectile.Owner.parent;
    }

    private void OnEnable()
    {
        TargetLock();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void TargetLock()
    {
        target = null;

        foreach (Transform ship in shipsParent)
        {
            if (ship.CompareTag(ignoreTag))
                continue;

            if (ship.gameObject.activeSelf == false)
                continue;

            target = ship;
            break;
        }
    }

    private void MoveToTarget()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - projectileTransform.position;
        projectileTransform.rotation = Quaternion.RotateTowards(projectileTransform.rotation, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg - 90f, Vector3.forward), autoTargetForce);
    }
}
