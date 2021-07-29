using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ControlOfThePossibilityOfUsingTheSelectedWeapon
{
    public static bool myWeapon(int typeOfWeapon, float wasteOfManaOnShot, float amountOfMana)
    {
        if (typeOfWeapon != 0)
        {
            return (amountOfMana < wasteOfManaOnShot) ? true : false;
        }

        return true;
    }

    public static void numberOfPossibleUses(ref Text numberOfPossibleUses, float wasteOfManaOnShot, float amountOfMana)
    {
        numberOfPossibleUses.text = (Mathf.Floor(amountOfMana / wasteOfManaOnShot)).ToString();
    }



}
