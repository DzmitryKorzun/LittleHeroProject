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



    private void Awake()
    {
        if (!singltone)
        {
            singltone = this;
           // DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
        recaveryHealth = ShopController.recoveryHealth;
        recaveryMana = ShopController.recoveryMana;
    }

    public void addCoin(int coinsReward)
    {
        money += coinsReward;
        EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
    }

    public void addHealthBottle(int cost)
    {
        if (cost <= money)
        {
            money -= cost;
            healthBottleCount++;
            EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
        }
    }

    public void addManaBottle(int cost)
    {
        if (cost <= money)
        {
            money -= cost;
            manaBottleCount++;
            EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
        }
    }

    public void useManaBottle()
    {
        if (manaBottleCount != 0)
        {
            manaBottleCount--;
            EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
            EventController.usingItemsFromInventoryEvent(1, recaveryMana);
        }

    }

    public void useHealthBottle()
    {
        if (healthBottleCount !=0)
        {
            healthBottleCount--;
            EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
            EventController.usingItemsFromInventoryEvent(0, recaveryHealth);
        }
    }

    public void ResetAll()
    {
        money = 10;
        manaBottleCount = 0;
        healthBottleCount = 0;
        EventController.InventoryStateChangeEvent(money, healthBottleCount, manaBottleCount);
    }






}
