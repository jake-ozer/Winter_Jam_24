using UnityEngine;

public class InventoryItemButton : MonoBehaviour
{
    public InventoryItem item;

    public void ShowDescription()
    {
        //call inventory manager and pass the item as param
        FindFirstObjectByType<InventoryManager>().ShowItemDescription(item);
    }

}
