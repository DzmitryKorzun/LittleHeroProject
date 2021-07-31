using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public int health—ost = 2;
    public int manaCost = 5;

    float healthRecovery = 25;
    float manaRecovery = 25;

    public delegate void MoneyChange();
    public event MoneyChange MoneyEvent;


    private void Start()
    {

    }

    public void BuyHealth()
    {
        Inventory.singltone.addHealthBottle(health—ost);
    }

    public void BuyMana()
    {
        Inventory.singltone.addManaBottle(manaCost);
    }
}
