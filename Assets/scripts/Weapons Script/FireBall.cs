using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, IWeapon
{
    private Vector3 coordinatesOfTheHitPoint;
    private bool isGotIt;
    public static float damage = 15;

    public void fire(Animator animAtack, Transform heroTransform)
    {
        RaycastHit hit;
        Ray ray = new Ray(heroTransform.position, heroTransform.forward);
        isGotIt = Physics.Raycast(ray, out hit);
        coordinatesOfTheHitPoint = hit.point;

        if (isGotIt && hit.collider.gameObject.tag == "Enemy")
        {

            AllEnemyController.damageTakenAgainstTheEnemy(hit.collider.gameObject.GetComponent<IOfEnemy>(), damage);
        }
    }
}
