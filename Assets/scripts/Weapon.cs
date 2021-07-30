using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{    
    void fire(float damage, Animator animAtack, Transform heroTransform);
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
        switch (selectedType)
        {
            case 0:
                weapon[0].fire(Sword.damage, anim, heroTransform);
                break;
            default:
                break;
        }
    }
}


public class Sword: IWeapon
{
    public static float damage = 10;
    public void fire(float damage, Animator animAtack, Transform heroTransform)
    {
        animAtack.Play("Attack1");
        RaycastHit hit;
        Ray ray = new Ray(heroTransform.position, heroTransform.forward);
        Debug.DrawRay(heroTransform.position, heroTransform.forward, Color.red, 10);

        if (Physics.Raycast(ray, out hit, 10f) && hit.collider.gameObject.tag == "Enemy")
        {
            AllEnemyController.damageTakenAgainstTheEnemy(hit.collider.gameObject.GetComponent<IOfEnemy>(), damage);
        }
    }
    
}

public class FireBall : IWeapon
{
    public void fire(float damage, Animator animAtack, Transform heroTransform)
    {

    }

}

public class Bomb : IWeapon
{
    public void fire(float damage, Animator animAtack, Transform heroTransform)
    {

    }
}

