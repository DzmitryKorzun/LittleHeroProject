using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ControlOfThePossibilityOfUsingTheSelectedWeapon
{

    public static void numberOfPossibleUses(ref Text numberOfPossibleUses, float wasteOfManaOnShot, float amountOfMana)
    {
        numberOfPossibleUses.text = (Mathf.Floor(amountOfMana / wasteOfManaOnShot)).ToString();
    }



}
