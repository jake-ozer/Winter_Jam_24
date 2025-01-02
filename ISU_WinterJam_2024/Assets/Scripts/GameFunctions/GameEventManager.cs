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
            { "take_sponge_letter", HasSpongeLetter }
        };
    }

    //triggers event based on pre-defined ID
    public void TriggerEvent(string eventID)
    {
        if (!eventDictionary.ContainsKey(eventID)) return;
        eventDictionary[eventID]();
    }

    //removes the letter from spongebob to patrick from the player's inventory
    private void HasSpongeLetter()
    {
        FindFirstObjectByType<PrerequisiteManager>().UpdatePreReq("sponge_letter", true);
        FindFirstObjectByType<InventoryManager>().RemoveItemFromInventory("Letter From Spongebob");
    }
}
