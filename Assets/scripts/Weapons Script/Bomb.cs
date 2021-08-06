using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IWeapon
{

    private float manaCost = 10;
    private float reloadTime = 5f;
    public GameObject bomb;
    public GameObject bombExplosionEffect;
    private SphereCollider collider;

    private void Start()
    {
        bombExplosionEffect.SetActive(false);
        collider = bomb.GetComponent<SphereCollider>();
    }

    public float getManaCostPerShot()
    {
        return manaCost;
    }

    public void fire(Animator animAtack, Transform heroTransform)
    {
        bomb = PoolWeapons.singltone.bombObj;
        bomb.SetActive(true);
        bomb.transform.position = heroTransform.position;
        bombExplosionEffect.transform.position = heroTransform.position;
        bombExplosionEffect.SetActive(false);
        Invoke("enableCollision", reloadTime);
        collider.enabled = false;        
    }

    private void enableCollision()
    {
        collider.enabled = true;
        Invoke("Explosion", 0.15f);
    }

    private void Explosion()
    {

        bomb.SetActive(false);
        bombExplosionEffect.SetActive(true);

    }





}
