using TMPro;
using UnityEngine;

public class SaveSlotButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slotName;

    public TextMeshProUGUI SlotName => slotName;
    public byte SaveId { get; set; }

    public void SelectSlot()
    {
        SaveMenu.SelectSaveSlot(SaveId);
    }
}
