using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class SkeletController : MonoBehaviour, IOfEnemy
{
    public static SkeletController singlton;
    private float damage = 20;
    public float health = 100;
    private Vector3 myHeroPos;
    private Transform myHeroTransform;
    private Transform enemyTransform;
    public float moveSpeed = 15f;
    private int killReward = 1;
    public GameObject HP_LineObject;
    public Image hp_Bar;
    private Camera mainCamera;
    private Animator animator;


    private void Awake()
    {
        singlton = this;
    }

    void Start()
    {
        PersonController.singlton.personMove += TheÑoordinatesOfTheHeroHaveChanged;
        myHeroTransform = PersonController.singlton.transform;
        myHeroPos = myHeroTransform.position;
        enemyTransform = this.transform;
        enemyTransform.DOLookAt(myHeroPos, 0f);
        mainCamera = Camera.main;
        HP_LineObject.transform.position = enemyTransform.position;
        animator = GetComponent<Animator>();
    }

    private void TheÑoordinatesOfTheHeroHaveChanged() // Called when the coordinates of the main character change
    {
        myHeroPos = myHeroTransform.position;
        enemyTransform.DOLookAt(myHeroPos, 0f);
    }

    private void FixedUpdate()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, myHeroPos, 1f * Time.deltaTime);
        HP_LineObject.transform.position = mainCamera.WorldToScreenPoint(enemyTransform.position);
    }

    public void takeDamage(float damage)
    {
        enemyTransform.DOJump(enemyTransform.localPosition, 1, 1, 1f);
        health = Mathf.Clamp(health - damage, 0, 100);
        hp_Bar.fillAmount = health/100;
        if (health == 0)
        {
            animator.Play("Die");

            Invoke("deactivateSkeleton", 1.7f);
            Inventory.singltone.addCoin(killReward);
        }
    }

    private void deactivateSkeleton()
    {
        Debug.Log("Îòêëþ÷àþ ñêåëåòà");
        this.gameObject.SetActive(false);
    }

    public float getTheDamageValueOfTheEnemy()
    {
        return damage;
    }
}
