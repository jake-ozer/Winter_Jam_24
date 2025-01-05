using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //This is the master class for all in-game events. Triggers for in game events will occur here

    private Dictionary<string, Action> eventDictionary;

    private void Start()
    {
        eventDictionary = new Dictionary<string, Action>
        {
            { "take_sponge_letter", HasSpongeLetter },
            { "award_badge", AwardBadge },
            { "talked_briny", TalkedBriny },
            { "remove_shelby_letters", RemoveShelbyLetters }
        };
    }

    //triggers event based on pre-defined ID
    public void TriggerEvent(string eventID)
    {
        if (!eventDictionary.ContainsKey(eventID)) return;
        eventDictionary[eventID]();
    }

    //increments badge count by one, furthering progressing in the game
    private void AwardBadge()
    {
        FindFirstObjectByType<NotificationManager>().ShowNotif("A sea dweller has agreed to help take down the machine!");
        //Debug.Log("badge awarded");
    }

    //removes the letter from spongebob to patrick from the player's inventory
    private void HasSpongeLetter()
    {
        FindFirstObjectByType<PrerequisiteManager>().UpdatePreReq("sponge_letter", true);
        FindFirstObjectByType<InventoryManager>().RemoveItemFromInventory("Letter From Spongebob");
    }

    private void TalkedBriny()
    {
        FindFirstObjectByType<PrerequisiteManager>().talkedBriny = true;
    }

    private void RemoveShelbyLetters()
    {
        FindFirstObjectByType<InventoryManager>().RemoveItemFromInventory("Shelby Letter 1");
        FindFirstObjectByType<InventoryManager>().RemoveItemFromInventory("Shelby Letter 2");
        FindFirstObjectByType<InventoryManager>().RemoveItemFromInventory("Shelby Letter 3");
    }

}
