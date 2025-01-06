using System.Collections.Generic;
using UnityEngine;

public class SetDressing : MonoBehaviour
{
    public List<Sprite> possibleSprites;
    public SpriteRenderer gfx;

    private void Start()
    {
        gfx.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
    }
}
