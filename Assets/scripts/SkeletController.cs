using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SkeletController : MonoBehaviour, IOfEnemy
{
    public static SkeletController singlton;
    private float damage = 10;
    public float health = 100;
    private Vector3 myHeroPos;
    private Transform myHeroTransform;
    private Transform enemyTransform;
    public float moveSpeed = 15f;
    private int killReward = 2;
    public GameObject HP_LineObject;
    private Transform HP_LineObjectTranform;
    public Image hp_Bar;
    private Camera mainCamera;
    public Animator animator;

    private float maxHP = 100;
    private float scale = 12;



    private void Awake()
    {
        singlton = this;
        DontDestroyOnLoad(this.gameObject);
        EventController.gameRepeat += unsubscribingFromAnEvent;
    }

    void Start()
    {
        EventController.bossFight += desable;

        myHeroTransform = PersonController.singlton.transform;
        myHeroPos = myHeroTransform.position;
        enemyTransform = this.transform;
        enemyTransform.DOLookAt(myHeroPos, 0f);
        mainCamera = Camera.main;
        HP_LineObjectTranform = HP_LineObject.GetComponent<Transform>();
        HP_LineObjectTranform.position = enemyTransform.position;
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        enemyTransform.DOLookAt(myHeroTransform.position, 0f);
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, myHeroTransform.position, 1f * Time.deltaTime);
        HP_LineObjectTranform.position = mainCamera.WorldToScreenPoint(enemyTransform.position);
    }

    private void unsubscribingFromAnEvent()
    {
        EventController.bossFight -= desable;
    }


    public void takeDamage(float damage)
    {
        enemyTransform.DOJump(enemyTransform.localPosition, 1, 1, 1f);
        health = Mathf.Clamp(health - damage, 0, maxHP);
        hp_Bar.fillAmount = health / maxHP;
        if (health == 0)
        {
            PersonController.singlton.killCounter();
            animator.Play("Die");
            Invoke("reloadSceleton", 1.7f);
            Inventory.singltone.addCoin(killReward);
        }
    }

    private void desable()
    {
        this.gameObject.SetActive(false);
    }

    private void reloadSceleton()
    {
        maxHP += 2;
        health = maxHP;
        damage += 2;
        animator.Play("Move");
        scale += 1;
        enemyTransform.DOScale(scale, 0.1f);
        hp_Bar.fillAmount = health / maxHP;
        enemyTransform.position = RandPosController.RandObjPos();
    }

    public float getTheDamageValueOfTheEnemy()
    {
        return damage;
    }
}
