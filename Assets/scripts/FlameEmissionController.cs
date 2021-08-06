using System.Collections.Generic;
using UnityEngine;

public class FlameEmissionController : MonoBehaviour
{
    private List<GameObject> flameEmissionListObj = new List<GameObject>();

    public GameObject flameEmissionObj;

    private int coutOfFlame = 5;
    public List<BoxCollider> collidersFlame;
    void Start()
    {
        for (int i=0; i< coutOfFlame; i++)
        {
            flameEmissionListObj.Add(Instantiate(flameEmissionObj, this.transform));
            flameEmissionListObj[i].SetActive(false);
            InvokeRepeating("flameLogic", 0, 5);
            collidersFlame.Add(flameEmissionListObj[i].GetComponent<BoxCollider>());
        }

    }

    public void flameLogic()
    {
        foreach (var item in collidersFlame)
        {
            item.enabled = false;
        }
        foreach (var item in flameEmissionListObj)
        {
            item.SetActive(false);
            item.SetActive(true);
            item.transform.position = RandPosController.RandObjPos();
        }
        Invoke("onCol", 0.8f);
    }

    private void onCol()
    {
        foreach (var item in collidersFlame)
        {
            item.enabled = true;
        }
    }






}
