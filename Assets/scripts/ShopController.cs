using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public int health—ost = 2;
    public int manaCost = 5;

    public delegate void MoneyChange();
    public event MoneyChange MoneyEvent;

    public void BuyHealth()
    {
        Inventory.singltone.addHealthBottle(health—ost);
    }

    public void BuyMana()
    {
        Inventory.singltone.addManaBottle(manaCost);
    }
}
