using TMPro;
using UnityEngine;

public class ShipChoosing : MonoBehaviour
{
    [Header("Ship unlock conditions")]
    [SerializeField] private int[] highScores;

    [Header("References")]
    [SerializeField] private GameObject LockedIcon;
    [SerializeField] public Ship[] playerShips;
    [SerializeField] private Transform turretPlaces;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI deffenceText;
    [SerializeField] private TextMeshProUGUI turretsCountText;
    private Transform shipsParent;

    public byte currentShip;
    private byte shipCount;
    private Ship ship;
    private Transform turret;

    public static ShipChoosing instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        shipsParent = transform;

        foreach (Ship ship in playerShips)
        {
            var newShip = Instantiate(ship, shipsParent);
            Destroy(newShip.GetComponent<PlayerControlls>());
            Destroy(newShip.GetComponent<HealthSystem>());
            Destroy(newShip.GetComponent<Rigidbody2D>());
            Destroy(newShip.GetComponent<BoxCollider2D>());

            var movement = newShip.GetComponent<MovementRotation>();
            movement.SetSize(10f);
            movement.flatteringPower = 6f;
            movement.parralaxPower = 6f;
            movement.rotationPower = 6f;

            newShip.transform.localPosition = Vector3.zero;
            foreach (var turret in newShip.GetTurrets())
                turret.SetState(false);
            newShip.gameObject.SetActive(false);
        }

        for (byte i = 0; i < shipsParent.childCount; i++)
        {
            if (ScorePointer.GetHighScore() >= highScores[i])
                continue;
            else
                shipsParent.GetChild(i).GetChild(1).GetComponent<DamageFlash>().SetColor(Color.black, true);
        }

        shipCount = (byte)shipsParent.childCount;
        currentShip = (byte)PlayerPrefs.GetInt("startShip");
        SetShip();
    }

    public void Next()
    {
        shipsParent.GetChild(currentShip).gameObject.SetActive(false);

        if (currentShip >= shipCount - 1)
            currentShip = 0;
        else
            currentShip++;

        SetShip();
    }

    public void Previous()
    {
        shipsParent.GetChild(currentShip).gameObject.SetActive(false);

        if (currentShip <= 0)
            currentShip = (byte)(shipCount - 1);
        else
            currentShip--;

        SetShip();
    }

    public void SetShip()
    {
        shipsParent.GetChild(currentShip).gameObject.SetActive(true);

        if (ScorePointer.HighScore >= highScores[currentShip])
        {
            PlayerPrefs.SetInt("startShip", currentShip);
            UpdateTurrets();
        }
        ApplyTextStats(currentShip);
    }

    public void UpdateTurrets()
    {
        ship = shipsParent.GetChild(currentShip).GetComponent<Ship>();

        foreach (Transform turret in turretPlaces)
            turret.gameObject.SetActive(false);

        for (byte i = 0; i < ship.GetTurretCount(); i++)
        {
            turret = turretPlaces.GetChild(i);

            turret.gameObject.SetActive(true);
            turret.position = ship.GetTurrets()[i].transform.position;

            ship.SetTurretData(i, SaveSystem.placedTurrets[i]);
        }
    }

    public void ApplyTextStats(byte currentShip)
    {
        bool isOpened = ScorePointer.HighScore >= highScores[currentShip];
        LockedIcon.SetActive(!isOpened);
        if (!isOpened)
        {
            healthText.text = "HP: ?";
            deffenceText.text = "DEF: ?";
            turretsCountText.text = "T: ?";
            return;
        }

        byte totalHealthModifier = new int();
        byte totalDeffenceModifier = new int();
        foreach (var device in SaveSystem.placedDevices)
        {
            if (device == null)
                continue;

            totalHealthModifier += device.health;
            totalDeffenceModifier += (byte)Mathf.RoundToInt(device.defence * 100);
        }

        var healthSystem = playerShips[currentShip].GetComponent<HealthSystem>();
        healthText.text = $"HP: {(healthSystem.GetStartHealth() + totalHealthModifier).ToString()}";
        deffenceText.text = $"DEF: {(healthSystem.GetDeffence() + totalDeffenceModifier).ToString()}";
        turretsCountText.text = $"T: {playerShips[currentShip].GetTurretCount().ToString()}";
    }
}
