using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolWeapons : MonoBehaviour
{
    public static PoolWeapons singltone;
    public GameObject bombObj;
    public GameObject bombExplosionEffect;
    public GameObject projectileObg;
    public GameObject fireBallObj;
    public List<GameObject> projectileList;
    public List<GameObject> fireBallList;
    private Transform parentTransform;
    private Transform heroTranform;
    private GameObject returnedObject;
    private void Awake()
    {
        if (!singltone)
        {
            singltone = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(bombObj);
    }


    private void Start()
    {
        heroTranform = PersonController.singlton.heroTransform;
        bombObj.SetActive(false);
        bombExplosionEffect.SetActive(false);
        parentTransform = this.transform;
        initProjectiles();
        initFireBall();

    }

    private void initProjectiles()
    {
        projectileList.Add(Instantiate(projectileObg, parentTransform));
        projectileList[0].SetActive(false);
        projectileList.Add(Instantiate(projectileObg, parentTransform));
        projectileList[1].SetActive(false);
        projectileList.Add(Instantiate(projectileObg, parentTransform));
        projectileList[2].SetActive(false);
    }

    private void initFireBall()
    {
        fireBallList.Add(Instantiate(fireBallObj, heroTranform));
        fireBallList[0].SetActive(false);
        fireBallList.Add(Instantiate(fireBallObj, heroTranform));
        fireBallList[1].SetActive(false);
        fireBallList.Add(Instantiate(fireBallObj, heroTranform));
        fireBallList[2].SetActive(false);
    }

    public GameObject getFreeProjectiles()
    {
        returnedObject = projectileList.Find(obj => obj.activeSelf == false);
        if (returnedObject != null)
        {
            return returnedObject;
        }
        else
        {
            returnedObject = Instantiate(projectileObg, parentTransform);
            projectileList.Add(returnedObject);
            returnedObject.SetActive(false);
            return returnedObject;
        }
    }

    public GameObject getFreeFireBall()
    {
        returnedObject = fireBallList.Find(obj => obj.activeSelf == false);
        if (returnedObject != null)
        {
            return returnedObject;
        }
        else
        {
            returnedObject = Instantiate(fireBallObj, heroTranform);
            projectileList.Add(returnedObject);
            returnedObject.SetActive(false);
            return returnedObject;
        }
    }

}
