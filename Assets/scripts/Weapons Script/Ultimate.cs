using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour, IWeapon
{
    private float manaCost = 50;
    public GameObject ultimateEffect;
    private CapsuleCollider collider;

    void Start()
    {
        ultimateEffect.SetActive(false);
        collider = PersonController.singlton.GetComponent<CapsuleCollider>();    }

    public void fire(Animator animAtack, Transform heroTransform)
    {
        PersonController.singlton.manaUseMethod(50);
        animAtack.Play("AttackSpecial");
        ultimateEffect.transform.position = heroTransform.position;
        ultimateEffect.SetActive(true);
        Invoke("UltimateAttackEffectBreaker", 2);
        collider.radius = 7;
        PersonController.singlton.isUltimate = true;
    }

    private void UltimateAttackEffectBreaker()
    {
        collider.radius = 0.7f;
        PersonController.singlton.isUltimate = false;
        ultimateEffect.SetActive(false);
        CancelInvoke("UltimateAttackEffectBreaker");
    }

    public float getManaCostPerShot()
    {
        return manaCost;
    }



}
