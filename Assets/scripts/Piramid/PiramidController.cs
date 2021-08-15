using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PiramidController : MonoBehaviour, IOfEnemy
{
    private float maxHP = 1000f;
    private float healthPoint = 1000f;
    private PersonController person;
    private Vector3 projectileStartPosition;
    public GameObject gun;
    private Vector3 shootDirection;
    private Transform myHeroTransform;
    private Vector3 meHeroPosition;
    public static float projectileSpeed = 3f;
    private float projectileFiringFrequency = 1f;
    public GameObject canvas;
    public Image healthLine;
    public GameObject winPanel;

    public delegate void gameWinDelegate();
    public event gameWinDelegate gameWin;



    private bool isBossFight = false;
    public float getTheDamageValueOfTheEnemy()
    {
        return 1;
    }

    public void takeDamage(float damage)
    {
        if (isBossFight)
        {
            healthPoint = Mathf.Clamp(healthPoint -= damage, 0, maxHP);
            healthLine.fillAmount = healthPoint / maxHP;
            if (healthPoint == 0)
            {
                winPanel.SetActive(true);
                gameWin?.Invoke();
            }
        }
    }

    private void Start()
    {
        projectileStartPosition = gun.transform.position;
        myHeroTransform = PersonController.singlton.transform;
        EventController.bossFight += bossModeActive;
        InvokeRepeating("Attack", 1f, projectileFiringFrequency);
    }

    public void repeatButton_Click()
    {

        winPanel.SetActive(false);
    }

    public void ExitButton_Click()
    {
        Application.Quit();
    }


    private void init()
    {
        healthPoint = 1000;
        isBossFight = false;
        canvas.SetActive(false);
    }


    private void Attack()
    {
        var projectile = PoolWeapons.singltone.getFreeProjectiles();
        projectile.transform.position = projectileStartPosition;

        meHeroPosition = myHeroTransform.position;
        projectile.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(projectile.transform.DOMove(meHeroPosition, projectileSpeed));
        seq.OnComplete(OnCompleteAttack);

        void OnCompleteAttack()
        {
            projectile.SetActive(false);
        }
    }

    private void bossModeActive()
    {
        canvas.SetActive(true);
        isBossFight = true;
    }

   
}
