using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void fire( Animator animAtack, Transform heroTransform);
}

public interface IGetWeaponType
{
    void TypeOfWeapon(IWeapon[] weapon, int selectedType);
}


public class WeaponController : IGetWeaponType
{
    private Animator anim;
    private Transform heroTransform;
    public WeaponController(Animator animIn, Transform heroTrIn)
    {
        this.anim = animIn;
        this.heroTransform = heroTrIn;
    }

    public void TypeOfWeapon(IWeapon[] weapon, int selectedType)
    {
        weapon[selectedType].fire(anim, heroTransform);
    }
}

