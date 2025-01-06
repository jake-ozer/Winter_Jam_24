using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public InventoryItem item;
    public AudioClip pickupSound;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(pickupSound);
            FindFirstObjectByType<InventoryManager>().AddItemToInventory(item);
            Destroy(gameObject);
        }
    }
}
