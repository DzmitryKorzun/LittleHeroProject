using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public int health—ost = 2;
    public int manaCost = 5;

    static public float recoveryHealth = 25;
    static public float recoveryMana = 30;



    public void BuyHealth()
    {
        Inventory.singltone.addHealthBottle(health—ost);
    }

    public void BuyMana()
    {
        Inventory.singltone.addManaBottle(manaCost);
    }
}
