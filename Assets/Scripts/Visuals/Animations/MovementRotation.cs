using UnityEngine;

public class MovementRotation : MonoBehaviour
{
    [SerializeField] private float size = 4;
    [SerializeField] private Transform layersParent;
    Transform[] layers;

    [Range(0, 6)]
    [SerializeField] public float flatteringPower = 2;

    [Range(0, 6)]
    [SerializeField] public float rotationPower = 3;

    [Range(0, 6)]
    [SerializeField] public float parralaxPower = 2;

    private Transform ship;

    private Vector3 scale = Vector3.one;
    private Quaternion rotation = Quaternion.identity;

    private float horizontalVelocity = new float();
    private float previousX = new float();
    private float velocity;

    private Vector3 layerParallaxOffset = new Vector2(0.005f, 0);
    private float time = 0;
    private float sin;

    private float clamp = 5f;

    private void Awake()
    {
        ship = transform;
        layers = new Transform[layersParent.childCount];

        for (byte i = 0; i < layers.Length; i++)
            layers[i] = layersParent.GetChild(i);
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        horizontalVelocity = CalculateHorizontalVelocity();
        DirectionRotate();

        if (time >= Mathf.PI * 2)
            time = 0;
        else
            time += Time.fixedDeltaTime;

        sin = Mathf.Sin(time);

        DirectionFlattening(horizontalVelocity + sin);
        LayersParallax(horizontalVelocity + sin);
    }

    public void SetSize(float value)
    {
        size = value;
    }

    private float CalculateHorizontalVelocity()
    {
        velocity = (ship.position.x - previousX) / Time.fixedDeltaTime;
        previousX = ship.position.x;
        return Mathf.Clamp(velocity, -clamp, clamp);
    }

    private void DirectionRotate()
    {
        if (rotationPower == 0)
            return;

        rotation.z = -horizontalVelocity * Time.fixedDeltaTime * rotationPower;
        ship.rotation = rotation;
    }

    private void DirectionFlattening(float force)
    {
        scale.x = 1 - Mathf.Abs(force * Time.fixedDeltaTime * flatteringPower);
        ship.localScale = scale * size;
    }

    private void LayersParallax(float force)
    {
        for (byte i = 0; i < layers.Length; i++)
            layers[i].position = ship.position + ((i) * layerParallaxOffset * force * parralaxPower);
    }
}
