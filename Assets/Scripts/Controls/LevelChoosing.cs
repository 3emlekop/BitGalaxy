using UnityEngine;
using UnityEngine.UI;

public class LevelChoosing : MonoBehaviour
{
    [SerializeField] private LevelLoader loader;
    [SerializeField] private Sprite[] levelSprites;
    [SerializeField] private Image levelPreview;
    private byte chosenLevel;

    private void Awake()
    {
        chosenLevel = 1;
        SetSprite(chosenLevel);
    }

    public void Play()
    {
        loader.LoadLevel(chosenLevel);
        gameObject.SetActive(false);
    }

    public void Next()
    {
        chosenLevel++;
        if (chosenLevel >= levelSprites.Length)
            chosenLevel = 1;
        SetSprite(chosenLevel);
    }

    public void Prev()
    {
        chosenLevel--;
        if(chosenLevel == 0)
            chosenLevel = (byte)(levelSprites.Length - 1);
        SetSprite(chosenLevel);
    }

    private void SetSprite(byte chosenLevel)
    {
        levelPreview.sprite = levelSprites[chosenLevel];
    }
}
