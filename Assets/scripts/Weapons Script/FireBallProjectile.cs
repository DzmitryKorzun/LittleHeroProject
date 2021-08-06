using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    public float damage = 100;



    private void OnTriggerEnter(Collider per)
    {
        Debug.Log(per);
        if (per.gameObject.tag == "Enemy")
        {
            per.gameObject.GetComponent<IOfEnemy>().takeDamage(damage);
        }
    }


}
