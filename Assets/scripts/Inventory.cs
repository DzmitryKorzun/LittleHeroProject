using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory singltone;
    public int money = 10;
    public int manaBottleCount = 0;
    public int healthBottleCount = 0;

    private float recaveryHealth;
    private float recaveryMana;

    public delegate void ChangingInventory(int money, int health, int mana);
    public event ChangingInventory InventoryStateChange;

    public delegate void UseInventory(int id, float value);
    public event UseInventory usingItemsFromInventory;

    private void Awake()
    {
        if (!singltone)
        {
            singltone = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
        recaveryHealth = ShopController.recoveryHealth;
        recaveryMana = ShopController.recoveryMana;
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

    public void useManaBottle()
    {
        if (manaBottleCount != 0)
        {
            manaBottleCount--;
            InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
            usingItemsFromInventory?.Invoke(1, recaveryMana);
        }

    }

    public void useHealthBottle()
    {
        if (healthBottleCount !=0)
        {
            healthBottleCount--;
            InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
            usingItemsFromInventory?.Invoke(0, recaveryHealth);
        }
    }

    public void ResetAll()
    {
        money = 10;
        manaBottleCount = 0;
        healthBottleCount = 0;
        InventoryStateChange?.Invoke(money, healthBottleCount, manaBottleCount);
    }






}
