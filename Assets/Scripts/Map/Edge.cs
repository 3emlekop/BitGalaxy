using UnityEngine;

public class Edge : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lineSpriteRenderer;
    [SerializeField] private Level firstLevel;
    [SerializeField] private Level secondLevel;

    public Color LineColor
    {
        get { return lineSpriteRenderer.color; }
        set { lineSpriteRenderer.color = value; }
    }


}