using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void fire( Animator animAtack, Transform heroTransform);
    float getManaCostPerShot();
}

public interface IGetWeaponType
{
    void TypeOfWeapon(IWeapon[] weapon, int selectedType);
}


public class WeaponController : IGetWeaponType
{
    private Animator anim;
    private Transform heroTransform;
    private float manaPool;
    public WeaponController(Animator animIn, Transform heroTrIn)
    {
        this.anim = animIn;
        this.heroTransform = heroTrIn;
    }

    public void TypeOfWeapon(IWeapon[] weapon, int selectedType)
    {
        manaPool = PersonController.singlton.manaPool;
        float manaForShoot = weapon[selectedType].getManaCostPerShot();
        if (manaForShoot != 0)
        {
            if (manaForShoot<= manaPool)
            {
                weapon[selectedType].fire(anim, heroTransform);

                PersonController.singlton.manaUseMethod(manaPool -= manaForShoot);
            }

        }
        else
        {
            weapon[selectedType].fire(anim, heroTransform);
        }

    }
}

