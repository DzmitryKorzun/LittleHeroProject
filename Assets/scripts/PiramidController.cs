using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidController : MonoBehaviour, IOfEnemy
{
    private float healthPoint = 10000f;

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
        
    }



}
