using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IOfEnemy
{
    void takeDamage(float damage);
    float getTheDamageValueOfTheEnemy();

}

public class AllEnemyController : MonoBehaviour
{
    static public void damageTakenAgainstTheEnemy(IOfEnemy enemy, float damage)
    {
        enemy.takeDamage(damage);
    }


}
