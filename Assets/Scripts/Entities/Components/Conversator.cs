using System.Collections;
using TMPro;
using UnityEngine;

public class Conversator : MonoBehaviour
{
    public delegate void OnNext();
    public event OnNext onNext;

    [SerializeField] public GameObject dialogBar;
    [SerializeField] public GameObject continueButton;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Audio")]
    [SerializeField] private AudioSource typeAudioSource;

    [HideInInspector]
    public readonly string[] secretEnemyReplics =
    {
        "....",
        "I thought I would die alone..",
        "I think you will understand everything only when you reach the end..",
        "Since from now on I can only die, I will put my hope in you and give you <color=#3bdbdbff>my weapon</color>..",
        "I hope you remember me when it's time to choose...."
    };


    [HideInInspector]
    public readonly string[] otherEndStageReplics =
    {
        "Hm, I think I trust you more now..",
        "I see you remember my request about not doing anything else..",
        "It's impressing several times more. You continue!!",
        "As far as I understand, you don't even think about who you are killing..",
        "I see you are hard to stop. It's good for my empi... ahem... forget it..",
        "Tens of thousands of coins pour from your campaigns! They will definitely go for the right things..",
        "All the previous ones also did everything I told them, but they were not so stubborn",
    };

    [HideInInspector]
    public readonly string lastEndSatgeReplic = "It's good for both of us! Keep it up!! And take your <color=#ff008cff>30 crystals</color>. You deserve them..";

    [HideInInspector]
    public readonly string[] endStageReplics =
    {
        "I'm impressed!!",
        "Take these <color=#ff008cff>30 crystals</color> as reward..",
        "Keep waiting, I'll let you know if there are new tasks..",
        "Now I have to hurry. But you can continue your fun. Bye!!"
    };

    [HideInInspector]
    public readonly string[] guideReplics =
    {
        "Well. I don't have time to explain everything, but I'll try to tell you the most important things if you want..",
        "And don't ask me my name or other stupid things, as others do..",
        "All you need to know about what's going on here is that we need to get one thing before <color=#3bdbdbff>the bad guys</color> do..",
        "Now let me show you how to prepare for the battle. Press this button to go to your inventory..",
        "Here you have three tabs with items where you store everything you picked up or bought. In general, these are turrets, devices, and modules..",
        "You need a weapon to kill your enemies, right? So press this button to go to the store and buy one!!",
        "All you have for now is the cheapest turret, so buy it..",
        "Now you need to install it on your ship..",
        "The last thing to do is to buy a slot for the device..",
        "That's it. I've talked too much. I hope you're smart enough to install the device or module on your own..",
        "Good luck!!"
    };

    [HideInInspector]
    public readonly string[] startTradeReplics =
    {
        "Hey, I see you're doing great! Right??",
        "Sorry, I've interrupted your fun..",
        "There are so many of them, ins't it??"
    };

    [HideInInspector]
    public readonly string[] secondTradeReplics =
    {
        "Anyway, I have some gifts for you! Of course not for free..",
        "I decided to give you more abilities to destroy those bad guys..",
        "I've brougth some interesting things. Do you want to take a look??"
    };

    [HideInInspector]
    public readonly string endTradeReplic = "See you later!!";

    private void Awake()
    {
        dialogBar.SetActive(false);
    }

    public void CloseDialog()
    {
        dialogText.text = string.Empty;
        dialogBar.SetActive(false);
    }

    public void OpenDialog(Vector3 pos)
    {
        dialogBar.SetActive(true);
        dialogBar.transform.position = pos;
    }

    public void Next()
    {
        onNext?.Invoke();
    }

    public IEnumerator AnimateText(string text, bool enableContinue)
    {
        if(enableContinue)
            continueButton.SetActive(false);
        if (text.Length >= 1)
        {
            for (int i = 0; i < text.Length; i++)
            {
                dialogText.text = text.Remove(i);
                typeAudioSource.Play();
                yield return null;
            }
        }
        if(enableContinue)
            continueButton.SetActive(true);
    }
}
