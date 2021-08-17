using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventController
{
    public delegate void HealthChangeEvent(float health);
    public static event HealthChangeEvent healthChange;

    public delegate void UseOfMana(float mana);
    public static event UseOfMana manaUse;

    public delegate void DeadEvent();
    public static event DeadEvent deadEvent;

    public delegate void ShoppingTrip(bool isEntered);
    public static event ShoppingTrip ShopApproach;

    public delegate void Boss();
    public static event Boss bossFight;

    public delegate void deadSceleton();
    public static event deadSceleton skeletDead;

    public delegate void repeatGame();
    public static event repeatGame gameRepeat;

    public delegate void ChangingInventory(int money, int health, int mana);
    public static event ChangingInventory InventoryStateChange;

    public delegate void UseInventory(int id, float value);
    public static event UseInventory usingItemsFromInventory;

    public delegate void MoneyChange();
    public static event MoneyChange MoneyEvent;


    public static void healthChangeEvent(float health)
    {
        healthChange?.Invoke(health);
    }

    public static void manaUseEvent(float mana)
    {
        manaUse?.Invoke(mana);
    }

    public static void heroDeadEvent()
    {
        deadEvent?.Invoke();
    }

    public static void ShopApproachEvent(bool isEntered)
    {
        ShopApproach?.Invoke(isEntered);
    }

    public static void bossFightEvent()
    {
        bossFight?.Invoke();
    }

    public static void skeletDeadEvent()
    {
        skeletDead?.Invoke();
    }

    public static void gameRestartEvent()
    {
        gameRepeat?.Invoke();
    }

    public static void InventoryStateChangeEvent(int money, int health, int mana)
    {
        InventoryStateChange?.Invoke(money, health, mana);
    }

    public static void usingItemsFromInventoryEvent(int id, float value)
    {
        usingItemsFromInventory?.Invoke(id, value);
    }

}
