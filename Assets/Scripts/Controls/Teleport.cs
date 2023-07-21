using UnityEngine;

public class Teleport : MonoBehaviour
{
    private PlayerControlls playerControlls;

    private Transform teleport;
    private Transform ship;

    private void Start()
    {
        teleport = transform;
        ship = teleport.parent;
        teleport.position = ship.position;
        playerControlls = ship.GetComponent<PlayerControlls>();

        EnableTeleport();
    }

    private void EnableTeleport()
    {
        playerControlls.SetMoveTime(1);
    }
}
