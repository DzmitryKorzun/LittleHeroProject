using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public static float damage = 15;
    public static float range = 5;
    public void fire(Animator animAtack, Transform heroTransform)
    {
        animAtack.Play("Attack1");
        RaycastHit hit;
        Ray ray = new Ray(heroTransform.position, heroTransform.forward);

        if (Physics.Raycast(ray, out hit, range) && hit.collider.gameObject.tag == "Enemy")
        {
            AllEnemyController.damageTakenAgainstTheEnemy(hit.collider.gameObject.GetComponent<IOfEnemy>(), damage);
        }
    }
}
