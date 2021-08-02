using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IWeapon
{
    private float damage;
    private float reloadTime = 5f;
    private GameObject bomb;


    public void fire(Animator animAtack, Transform heroTransform)
    {
        bomb = PoolWeapons.singltone.bombObj;
        bomb.SetActive(true);
        bomb.transform.position = heroTransform.position;
        Invoke("ActivationFireButtonAndExplosion", reloadTime);
    }

    private void ActivationFireButtonAndExplosion()
    {
        Debug.Log("Взрыв");
    }
}
