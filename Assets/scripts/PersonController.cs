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

    private CharacterController _charController;

    public delegate void MoveAction();
    public event MoveAction personMove;

    public delegate void HealthChangeEvent(float health);
    public event HealthChangeEvent healthChange;

    public delegate void UseOfMana(float mana);
    public event UseOfMana manaUse;

    public delegate void DeadEvent();
    public event DeadEvent deadEvent;

    public delegate void ShoppingTrip(bool isEntered);
    public event ShoppingTrip ShopApproachEvent;

    public delegate void Boss();
    public event Boss bossFight;

    public delegate void deadSceleton();
    public event deadSceleton skeletDead;

    public delegate void repeatGame();
    public event repeatGame gameRepeat;

    private void Awake()
    {
        if (!singlton)
        {
            singlton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        heroTransform = this.GetComponent<Transform>();
    }


    void Start()
    {
        startPos = new Vector3(10,0,10);
        manaUse += useMana;
        cam = Camera.main;
        _charController = GetComponent<CharacterController>();
        myJoystick = joystickControllerObj.GetComponent<JoystickController>();
        heroTransform = this.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        ray.direction = Vector3.forward;
        camTransform = cam.GetComponent<Transform>();
        heroTransform.position = startPos;
        Inventory.singltone.usingItemsFromInventory += theEffectOfUsingInventoryItems;
        
    }

    void FixedUpdate()
    {
        MovementLogic();
        if (isMove) personMove?.Invoke();
    }

    private void MovementLogic()
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

    private void useMana(float value)
    {
        this.manaPool = value;
    }

    public void init()
    {
        Inventory.singltone.ResetAll();
        gameRepeat?.Invoke();
        manaPool = 100;
        health = 100;
        heroTransform.position = startPos;
        maxHealth = 100;
        kills = 0;
        manaUse?.Invoke(100);
        healthChange?.Invoke(100);
        animator.Play("Idle");
    }


    public void killCounter()
    {
        kills++;
        ultimateDamage += 5;
        maxHealth += 10;
        skeletDead?.Invoke();
        StopCoroutine("DamageOverTimeCoroutine");
        if (kills >= countKillsForBossFight)
        {
            bossFight.Invoke();
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
            ShopApproachEvent?.Invoke(true);
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
            ShopApproachEvent?.Invoke(false);
        }
    }

    public void manaUseMethod(float mana)
    {
        manaUse?.Invoke(mana);
    }

    public IEnumerator DamageOverTimeCoroutine()
    {

        while (true)
        {
            health = Mathf.Clamp(health - damageToTheHeroFromTheEnemy, 0, maxHealth);
            healthChange.Invoke(health);
            isDeath();
            yield return new WaitForSeconds(1);

        }
    }

    private void isDeath()
    {
        if (health == 0)
        {
            deadEvent?.Invoke();
            animator.Play("Death");
        }

    }

    private void theEffectOfUsingInventoryItems(int id, float value)
    {
        switch (id)
        {
            case 0:
                health += value;
                healthChange.Invoke(health);
                break;

            case 1:
                manaPool += value;
                manaUse?.Invoke(manaPool);
                break;

            default:

                break;
        }
    }

    public void takingProjectileDamage(float value)
    {
        health = Mathf.Clamp(health - value, 0, maxHealth);
        healthChange.Invoke(health);
        isDeath();
    }


}
