using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private Transform text;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        text = transform;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (textMeshPro.alpha < 0.1f)
        {
            Destroy(gameObject);
            return;
        }

        text.position += Time.deltaTime * Vector3.up * speed;
        textMeshPro.alpha -= Time.fixedDeltaTime;
        text.localScale -= Vector3.one * Time.deltaTime * speed;
    }
}
