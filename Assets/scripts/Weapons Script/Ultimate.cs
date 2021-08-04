using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour, IWeapon
{
    private float manaCost = 50;
    public GameObject ultimateEffect;


    void Start()
    {
        ultimateEffect.SetActive(false);
    }

    public void fire(Animator animAtack, Transform heroTransform)
    {
        PersonController.singlton.manaUseMethod(50);
        animAtack.Play("AttackSpecial");
        ultimateEffect.transform.position = heroTransform.position;
        ultimateEffect.SetActive(true);
        Invoke("UltimateAttackEffectBreaker", 3);
    }

    private void UltimateAttackEffectBreaker()
    {
        ultimateEffect.SetActive(false);
        CancelInvoke("UltimateAttackEffectBreaker");
    }


    public float getManaCostPerShot()
    {
        return manaCost;
    }



}
