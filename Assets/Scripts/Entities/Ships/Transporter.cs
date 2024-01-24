using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField] private Transform cargosParent;

    private Transform transporterTransform;
    private GameObject transporterGameObject;
    private byte activateCount;

    private void Awake()
    {
        transporterTransform = transform;
        transporterGameObject = gameObject;
    }

    private void OnEnable()
    {
        activateCount = (byte)Random.Range(0, cargosParent.childCount);

        for(byte i = 0; i < cargosParent.childCount; i++)
        {
            cargosParent.GetChild(i).gameObject.SetActive(false);
            if(i <= activateCount)
                cargosParent.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(transporterTransform.position.y <= 7)
            transporterTransform.position += Vector3.up * Time.deltaTime;
        else if(transporterGameObject.activeSelf)
            transporterGameObject.SetActive(false);
    }
}
