using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEmissionCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 50;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            PersonController.singlton.takingProjectileDamage(damage);
        }
    }



}
