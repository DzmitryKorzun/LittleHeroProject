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
    [HideInInspector] public GameObject ultimateEffect;
    [HideInInspector] public float damage;

    private int hero_Lvl;
    public float speed = 5f;
    public GameObject joystickControllerObj;

    private Ray ray;

    public bool isMove { set; private get; }
    private int kills;
    private int deaths;
    private JoystickController myJoystick;
    private Camera cam;
    private Transform camTransform;
    private int cameraDistans = 10;

    private float gravity = -9.8f;
    protected float wasteOfManaOnUlt = 50;
    private Vector3 direct;
    private Vector3 moveVector = new Vector3();


    private CharacterController _charController;

    public delegate void MoveAction();
    public event MoveAction personMove;

    public delegate void TakingDamageEvent(float health);
    public event TakingDamageEvent takingDamage;

    public delegate void UseOfMana(float mana);
    public event UseOfMana manaUse;

    public delegate void DeadEvent();
    public event DeadEvent deadEvent;

    private void Awake()
    {
        singlton = this;
        heroTransform = this.GetComponent<Transform>();
    }


    void Start()
    {
        ultimateEffect.SetActive(false);
        cam = Camera.main;
        _charController = GetComponent<CharacterController>();
        myJoystick = joystickControllerObj.GetComponent<JoystickController>();
        heroTransform = this.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        ray.direction = Vector3.forward;
        camTransform = cam.GetComponent<Transform>();
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

    private void EnemyAtake()
    {
        health = Mathf.Clamp(health - 20, 0, 100);
        takingDamage.Invoke(health);
        if (health == 0)
        {
            deadEvent?.Invoke();
            RestartGameLvl();
        }
    }

    private void RestartGameLvl()
    {
        animator.Play("Death");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            InvokeRepeating("EnemyAtake", 1, 1);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            CancelInvoke("EnemyAtake");
        }
    }

    public void SimpleAttack()
    {

    }
    public void Attack2() //Ultimate
    {
        manaPool = Mathf.Clamp(manaPool - wasteOfManaOnUlt, 0, 100);
        manaUse?.Invoke(manaPool);
        animator.Play("AttackSpecial");
        ultimateEffect.transform.position = heroTransform.position;
        ultimateEffect.SetActive(true);
        InvokeRepeating("UltimateAttackEffectBreaker", 5, 5);
    }

    private void UltimateAttackEffectBreaker()
    {
        ultimateEffect.SetActive(false);
        CancelInvoke("UltimateAttackEffectBreaker");
    }

}
