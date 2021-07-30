using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IOfEnemy
{
    void takeDamage(float damage);
}

public class AllEnemyController : MonoBehaviour
{
    static IOfEnemy enemy;

    static public void damageTakenAgainstTheEnemy(IOfEnemy enemy, float damage)
    {
        enemy.takeDamage(damage);
    }







    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
