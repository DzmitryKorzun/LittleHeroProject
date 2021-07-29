using System.Collections.Generic;
using UnityEngine;

public class FlameEmissionController : MonoBehaviour
{
    private List<GameObject> flameEmissionListObj = new List<GameObject>();

    public GameObject flameEmissionObj;

    private int coutOfFlame = 20;
    private int numberOfCurrentlyActiveFires = 5;

    private int[,] coordinateArray = new int[,] {
        { 98,482}, {127,500}, {96,527}, {125,547},
        {120,565}, {141,587}, {171,576}, {188,558},
        {204,544}, {213,569}, {236,603}, {281,621},
        {298,575}, {321,548}, {369, 611}, {392,576},
        {404,546}, {469,561 }, {449, 523}, {334, 523}};

    void Start()
    {
        for (int i=0; i< coutOfFlame; i++)
        {
            flameEmissionListObj.Add(Instantiate(flameEmissionObj, new Vector3(coordinateArray[i,0], 0, coordinateArray[i, 1]), Quaternion.identity));
            flameEmissionListObj[i].transform.parent = transform;
            flameEmissionListObj[i].SetActive(false);
            InvokeRepeating("flameLogic", 0, 5);
        }
    }

    public void flameLogic()
    {
        deactivateAllFlame();
        activeRandomFlame();
    }

    private void deactivateAllFlame()
    {
        foreach (GameObject i in flameEmissionListObj)
        {
            i.SetActive(false);
        }
    }

    private void activeRandomFlame()
    {
        for (int i = 0; i < numberOfCurrentlyActiveFires; i++)
        {
            flameEmissionListObj[Random.RandomRange(0, coutOfFlame)].SetActive(true);
        }
    }


}
