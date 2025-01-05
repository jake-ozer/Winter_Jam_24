using System.Collections.Generic;
using System;
using UnityEngine;

public class PrerequisiteManager : MonoBehaviour
{
    //This is the master class for all dialogue prerequisites. Logic will be individualized based on the conditions needed

    private Dictionary<string, Func<bool>> condDictionary;
    private Dictionary<string, bool> hasOccured;
    public bool talkedBriny = false;

    private void Start()
    {
        condDictionary = new Dictionary<string, Func<bool>>
        {
            { "sponge_letter", HasSpongeLetter },
            { "talk_briny", CheckTalkBriny },
            {"shelby_letters", ShelbyLetters }
        };

        hasOccured = new Dictionary<string, bool>();
    }

    //checks prerequisite condition based on ID
    public bool CheckPrerequisite(string prereqID)
    {
        if (!condDictionary.ContainsKey(prereqID))
        {
            return false;
        }
        if (hasOccured.ContainsKey(prereqID) && hasOccured[prereqID])
        {
            return false;
        }
        return condDictionary[prereqID]();
    }

    //called by GameEventManager to indicate that a prereq is no longer valid
    public void UpdatePreReq(string id, bool val)
    {
        hasOccured[id] = val;
    }


    //checks to see if the letter from spongebob is in the player's inventory
    private bool HasSpongeLetter()
    {
        return FindFirstObjectByType<InventoryManager>().ItemInInventory("Letter From Spongebob");
    }

    //checks to see if player has talked to briny
    private bool CheckTalkBriny()
    {
        return talkedBriny;
    }

    //checks to see if the player has all 3 of shelby's letters in their inventory
    private bool ShelbyLetters()
    {
        bool val = FindFirstObjectByType<InventoryManager>().ItemInInventory("Shelby Letter 1")
            && FindFirstObjectByType<InventoryManager>().ItemInInventory("Shelby Letter 2")
                && FindFirstObjectByType<InventoryManager>().ItemInInventory("Shelby Letter 3");
        return val;
    }

}
