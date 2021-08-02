using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolWeapons : MonoBehaviour
{
    public static PoolWeapons singltone;
    public GameObject bombObj;
    public GameObject bombExplosionEffect;

    private void Awake()
    {
        singltone = this;
    }


    private void Start()
    {
        bombObj.SetActive(false);
        bombExplosionEffect.SetActive(false);
    }


}
