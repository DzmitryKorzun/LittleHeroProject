using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory singltone;
    public int money = 0;
    public int manaBottleCount = 0;
    public int healthBottleCount = 0;

    public delegate void ChangingInventory(int money, int health, int mana);
    public event ChangingInventory InventoryStateChange;

    private void Awake()
    {
        singltone = this;
    }
    private void Start()
    {
        InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
    }

    public void addCoin(int coinsReward)
    {
        money += coinsReward;
        InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
    }

    public void addHealthBottle(int cost)
    {

        if (cost <= money)
        {
            money -= cost;
            healthBottleCount++;
            InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
        }
    }

    public void addManaBottle(int cost)
    {
        if (cost <= money)
        {
            money -= cost;
            manaBottleCount++;
            InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
        }
    }








}
