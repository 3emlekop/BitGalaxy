using UnityEngine;

public class DropScanner : MonoBehaviour
{
    [SerializeField] private float cooldown = 5;
    private Transform shipsParent;
    private Color transparent = new Color(1, 1, 1, 0);
    private Transform scanner;

    private void Start()
    {
        scanner = transform;
        shipsParent = scanner.parent.parent;
        Timer.Create(scanner, Scan, cooldown, true);
    }

    private void Scan()
    {
        DropHandler dropHandler;
        for (byte i = 0; i < shipsParent.childCount; i++)
        {
            var ship = shipsParent.GetChild(i);
            if (ship.CompareTag("Player"))
                continue;

            if (ship.TryGetComponent<DropHandler>(out dropHandler))
                LightShip(ship, dropHandler.HasDrop());
        }
    }

    private void LightShip(Transform ship, bool hasDrop)
    {
        if (hasDrop)
            ship.GetComponent<SpriteRenderer>().color = Color.white;
        else
            ship.GetComponent<SpriteRenderer>().color = transparent;
    }
}
