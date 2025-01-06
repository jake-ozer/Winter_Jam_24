using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public List<Sprite> possibleSprites;
    public SpriteRenderer gfx;
    public AudioClip pickupSound;

    private void Start()
    {
        gfx.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(pickupSound);
            FindFirstObjectByType<MoneyManager>().AddMoney(1);
            Destroy(gameObject);
        }
    }
}
