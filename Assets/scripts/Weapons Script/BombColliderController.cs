using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombColliderController : MonoBehaviour
{
    public float damage = 120;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<IOfEnemy>().takeDamage(damage);
        }
    }
}
