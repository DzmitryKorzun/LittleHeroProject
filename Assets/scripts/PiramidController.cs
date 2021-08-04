using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PiramidController : MonoBehaviour, IOfEnemy
{
    private float healthPoint = 10000f;
    private PersonController person;
    private Vector3 projectileStartPosition;
    public GameObject gun;
    private Vector3 shootDirection;
    private Transform myHeroTransform;
    private Vector3 meHeroPosition;
    public float projectileSpeed = 3f;
    private float projectileFiringFrequency = 1f;
    public float getTheDamageValueOfTheEnemy()
    {
        return 1;
    }

    public void takeDamage(float damage)
    {
        healthPoint = Mathf.Clamp(healthPoint =- damage, 0, 10000f);
    }

    private void Start()
    {
        person = PersonController.singlton.GetComponent<PersonController>();
        projectileStartPosition = gun.transform.position;
        myHeroTransform = PersonController.singlton.transform;
        InvokeRepeating("Attack", 1f, projectileFiringFrequency);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PersonController.singlton.cameraDistans = 20;
            projectileSpeed = 1f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PersonController.singlton.cameraDistans = 10;
            projectileSpeed = 3f;
        }
    }

    private void Attack()
    {
        var projectile = PoolWeapons.singltone.getFreeProjectiles();
        projectile.transform.position = projectileStartPosition;

        meHeroPosition = myHeroTransform.position;
        projectile.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(projectile.transform.DOMove(meHeroPosition, projectileSpeed));
        seq.OnComplete(OnCompleteAttack);

        void OnCompleteAttack()
        {
            projectile.SetActive(false);
        }
    }



   
}
