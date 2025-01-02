using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public InventoryItem item;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            FindFirstObjectByType<InventoryManager>().AddItemToInventory(item);
            Destroy(gameObject);
        }
    }
}
