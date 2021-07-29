using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public static EnemyController singlton;
    public float health = 100;
    private Vector3 myHeroPos;
    private Transform myHeroTransform;
    private Transform enemyTransform;
    //private Animator enemyAnimator;
    public float moveSpeed = 15f;

    private void Awake()
    {
        singlton = this;


    }

    void Start()
    {
        PersonController.singlton.personMove += The—oordinatesOfTheHeroHaveChanged;
        myHeroTransform = PersonController.singlton.transform;
        myHeroPos = myHeroTransform.position;
        enemyTransform = this.transform;
        enemyTransform.DOLookAt(myHeroPos, 0f);
    }

    private void The—oordinatesOfTheHeroHaveChanged() // Called when the coordinates of the main character change
    {
        myHeroPos = myHeroTransform.position;
        enemyTransform.DOLookAt(myHeroPos, 0f);
    }



    private void FixedUpdate()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, myHeroPos, 1f * Time.deltaTime);
    }

    public void takingDamage(float damage)
    {
        Vector3 vec = new Vector3(0.0f, 0.0f, 3.0f);
        enemyTransform.DOJump(enemyTransform.localPosition, 1, 1, 1f);
        health = Mathf.Clamp(health - damage, 0, 100);
        if (health == 0)
        {
            this.gameObject.SetActive(false);
        }
    }




}
