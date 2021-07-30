using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Button healthButtonShop;
    public Button manaButtonShop;
    public Text healthTextShop;
    public Text manaTextShop;

    int health—ost = 2;
    int manaCost = 5;

    float healthRecovery = 25;
    float manaRecovery = 25;

    public delegate void MoneyChange();
    public event MoneyChange MoneyEvent;


    private void Start()
    {

    }

    void BuyHealth()
    {

        //PersonController.singlton.
        //healthChange.Invoke(health);
    }


}
