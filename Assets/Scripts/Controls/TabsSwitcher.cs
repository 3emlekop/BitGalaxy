using UnityEngine;
using UnityEngine.UI;

public class TabsSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private Button[] buttons;
    public void SwitchTab(int index)
    {
        foreach (var tab in tabs) { tab.SetActive(false); }
        tabs[index].SetActive(true);

        foreach (var button in buttons) { button.interactable = true; }
        buttons[index].interactable = false;
    }
}