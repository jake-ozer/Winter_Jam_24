using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public GameObject itemsParent;
    public TextMeshProUGUI descriptionText;
    public GameObject itemPrefab;
    public List<GameObject> inventoryItemGOs;

    private void Start()
    {
        descriptionText.text = "";
        inventoryItemGOs = new List<GameObject>();
    }

    public void AddItemToInventory(InventoryItem item)
    {
        FindFirstObjectByType<NotificationManager>().ShowNotif(item.itemName + " has been added to your inventory");
        var itemUI = Instantiate(itemPrefab);
        itemUI.transform.SetParent(itemsParent.transform);
        itemUI.GetComponent<InventoryItemButton>().item = item;
        itemUI.GetComponent<Image>().sprite = item.icon;
        inventoryItemGOs.Add(itemUI);
    }

    public void RemoveItemFromInventory(string itemName)
    {
        FindFirstObjectByType<NotificationManager>().ShowNotif(itemName + " has been removed from your inventory");
        var itemToDelete = inventoryItemGOs.Find(x => x.GetComponent<InventoryItemButton>().item.itemName == itemName);
        inventoryItemGOs.Remove(itemToDelete);
        Destroy(itemToDelete);
    }

    public void ShowItemDescription(InventoryItem item)
    {
        descriptionText.text = item.itemName+": "+item.description;
    }

    public void ClearDescription()
    {
        descriptionText.text = "";
    }

    public bool ItemInInventory(string itemName)
    {
        foreach(var item in inventoryItemGOs)
        {
            if(item.GetComponent<InventoryItemButton>().item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
