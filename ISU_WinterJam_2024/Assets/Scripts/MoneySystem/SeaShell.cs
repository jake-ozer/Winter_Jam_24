using UnityEngine;

public class SeaShell: MonoBehaviour
{
    public int shellValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null) {
            FindFirstObjectByType<MoneyManager>().AddMoney(shellValue);
            Debug.Log(shellValue + " shells added");
            Destroy(this.gameObject);
        }
    }
}
