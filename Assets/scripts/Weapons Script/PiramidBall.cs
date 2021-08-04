using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidBall : MonoBehaviour
{
    public float damage = 30f;

    private void OnTriggerEnter(Collider per)
    {
        if (per.gameObject.tag == "Player")
        {
            PersonController.singlton.takingProjectileDamage(damage);
        }
    }


}
