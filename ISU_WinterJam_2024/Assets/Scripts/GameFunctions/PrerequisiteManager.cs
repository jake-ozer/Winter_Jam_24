using System.Collections.Generic;
using System;
using UnityEngine;

public class PrerequisiteManager : MonoBehaviour
{
    //This is the master class for all dialogue prerequisites. Logic will be individualized based on the conditions needed

    private Dictionary<string, Func<bool>> condDictionary;
    private Dictionary<string, bool> hasOccured;

    private void Start()
    {
        condDictionary = new Dictionary<string, Func<bool>>
        {
            { "sponge_letter", HasSpongeLetter }
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


}
