using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private Transform text;
    TextMeshProUGUI textMeshPro;

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

        text.position += Time.deltaTime * Vector3.up * 1.5f;

        textMeshPro.alpha -= Time.fixedDeltaTime;

        text.localScale -= Vector3.one * Time.deltaTime * 1.5f;
    }
}
