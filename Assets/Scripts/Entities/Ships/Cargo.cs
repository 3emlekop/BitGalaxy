using UnityEngine;

public class Cargo : MonoBehaviour
{
    [Header("Drop")]
    [SerializeField] private SpriteRenderer dropVisualizeIcon;
    [SerializeField] private SpriteRenderer dropVisualizeOutline;
    [SerializeField] private DropHandler dropHandler;

    [Header("Health references")]
    [SerializeField] private HealthSystem cargohealthSystem;
    [SerializeField] private HealthSystem parentCargoHealthSystem;

    private void Start()
    {
        parentCargoHealthSystem.onDeath += Die;
    }

    private void Die(bool withLoot)
    {
        cargohealthSystem.Die(true);
    }

    private void OnEnable()
    {
        if (dropHandler.HasDrop())
            dropVisualizeIcon.sprite = dropHandler.GetDropItem().Sprite;
        else
            dropVisualizeIcon.sprite = null;

        dropVisualizeOutline.sprite = dropVisualizeIcon.sprite;
    }
}
