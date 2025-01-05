using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int curMoney;

    public void RemoveMoney(int amount)
    {
        if (curMoney < amount)
        {
            Debug.Log("Not Enough Money");
        }
        else
        {
            curMoney -= amount;
        }
    }

    public void AddMoney(int amount)
    {
        curMoney += amount;
    }

}
