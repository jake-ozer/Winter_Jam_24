using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int amount;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        amount = 0;
    }

    public int GetAmount()
    {
        return amount;
    }

    private void Update()
    {
        moneyText.text = amount.ToString();
    }

    public void AddMoney(int a)
    {
        amount += a;
    }

    public void RemoveMoney(int a)
    {
        amount -= a;
    }

}
