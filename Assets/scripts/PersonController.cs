using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PersonController : MonoBehaviour
{
    public static PersonController singlton { get; private set; }
    private float health = 100;

    [HideInInspector] public float manaPool = 100;
    [HideInInspector] public Transform heroTransform;
    [HideInInspector] public Animator animator;
    [HideInInspector] public float damage;


    public float speed = 5f;
    public GameObject joystickControllerObj;

    private Ray ray;
    private Vector3 startPos;
    public bool isMove { set; private get; }
    private int kills = 0;
    private JoystickController myJoystick;
    private Camera cam;
    private Transform camTransform;
    private float ultimateDamage = 80f;
    public int cameraDistans = 10;
    public bool isUltimate = false;
    private float maxHealth = 100;
    private float countKillsForBossFight = 50;
    private float gravity = -9.8f;
    protected float wasteOfManaOnUlt = 50;
    private Vector3 direct;
    private Vector3 moveVector = new Vector3();
    private Transform parentTransform;
    private float damageToTheHeroFromTheEnemy;
    private bool isShopEnter = false;
    private CharacterController _charController;
    public static bool isVulnerable = true;



    private void Awake()
    {
        if (!singlton)
        {
            singlton = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        heroTransform = this.GetComponent<Transform>();
        EventController.gameRepeat += unsubscribingFromAnEvent;
    }


    void Start()
    {
        EventController.manaUse += useMana;
        EventController.usingItemsFromInventory += theEffectOfUsingInventoryItems;
        startPos = new Vector3(10, 0, 10);
        cam = Camera.main;
        _charController = GetComponent<CharacterController>();
        myJoystick = joystickControllerObj.GetComponent<JoystickController>();
        heroTransform = this.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        ray.direction = Vector3.forward;
        camTransform = cam.GetComponent<Transform>();
        heroTransform.position = startPos;        
    }

    void FixedUpdate()
    {
        moveVector = Vector3.zero;
        moveVector.x = myJoystick.Horizontal() * speed;
        moveVector.z = myJoystick.Vertical() * speed;

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            direct = Vector3.RotateTowards(heroTransform.forward, moveVector, speed, 0.0f);
            heroTransform.rotation = Quaternion.LookRotation(direct);
        }

        moveVector.y = gravity;
        _charController.Move(moveVector * Time.deltaTime);

        if (isMove)
        {
            camTransform.DOMove(new Vector3(heroTransform.position.x, cameraDistans, heroTransform.position.z), 1f);
            animator.SetBool("Run", true);
        }
        else
        {
            camTransform.DOMove(new Vector3(heroTransform.position.x, 9, heroTransform.position.z), 1f);
            animator.SetBool("Run", false);
        }

    }

    private void unsubscribingFromAnEvent()
    {
        EventController.manaUse -= useMana;
        EventController.usingItemsFromInventory -= theEffectOfUsingInventoryItems;
    }


    private void useMana(float value)
    {
        this.manaPool = value;
    }



    public void killCounter()
    {
        kills++;
        ultimateDamage += 5;
        maxHealth += 10;
        EventController.skeletDeadEvent();
        StopCoroutine("DamageOverTimeCoroutine");
        if (kills >= countKillsForBossFight)
        {
            EventController.bossFightEvent();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isUltimate)
        {
            damageToTheHeroFromTheEnemy = collision.gameObject.GetComponent<IOfEnemy>().getTheDamageValueOfTheEnemy();
            StartCoroutine("DamageOverTimeCoroutine");
        }
        if (collision.gameObject.tag == "Shop")
        {
            EventController.ShopApproachEvent(true);
            isVulnerable = false;
        }
        if (collision.gameObject.tag == "Enemy" && isUltimate)
        {
            collision.gameObject.GetComponent<IOfEnemy>().takeDamage(ultimateDamage);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StopCoroutine("DamageOverTimeCoroutine");
        }
        if (collision.gameObject.tag == "Shop")
        {
            isVulnerable = true;
            EventController.ShopApproachEvent(false);
        }
    }

    public void manaUseMethod(float mana)
    {
        EventController.manaUseEvent(mana);
    }

    public IEnumerator DamageOverTimeCoroutine()
    {
        while (true)
        {
            health—hangeAfterTakingDamage(damageToTheHeroFromTheEnemy);
            isDeath();
            yield return new WaitForSeconds(1);

        }
    }

    private void isDeath()
    {
        if (health == 0)
        {
            EventController.heroDeadEvent();
            animator.Play("Death");
        }

    }

    private void theEffectOfUsingInventoryItems(int id, float value)
    {
        switch (id)
        {
            case 0:
                health += value;
                EventController.healthChangeEvent(health);
                break;

            case 1:
                manaPool += value;
                EventController.manaUseEvent(manaPool);
                break;

            default:

                break;
        }
    }

    public void takingProjectileDamage(float value)
    {
        health—hangeAfterTakingDamage(value);
        isDeath();
    }

    private void health—hangeAfterTakingDamage(float value)
    {
        if (isVulnerable)
        {
            health = Mathf.Clamp(health - value, 0, maxHealth);
            EventController.healthChangeEvent(health);
        }

    }
}
