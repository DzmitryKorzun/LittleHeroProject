using UnityEngine;
using DG.Tweening;

public class FireBall : MonoBehaviour, IWeapon
{
    public static float damage = 15;
    public static float manaCost = 5f;
    public static float speedFireBall = 5f;
    public void fire(Animator animAtack, Transform heroTransform)
    {

        var projectile = PoolWeapons.singltone.getFreeFireBall();
        projectile.transform.position = heroTransform.position;
        projectile.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(projectile.transform.DOLocalMoveZ(50, speedFireBall));
        seq.OnComplete(killMoveAndDeactivateFireBall);

        void killMoveAndDeactivateFireBall()
        {
            projectile.SetActive(false);
        }

    }
    public float getManaCostPerShot()
    {
        return manaCost;
    }
}
