using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loc1_LogicController : MonoBehaviour
{

    private int countOfStone  = 25;
    private int countOfGrass = 25;

    [SerializeField]
    private List<GameObject> stone = new List<GameObject>();
    [SerializeField]
    private List<GameObject> grass = new List<GameObject>();

    public GameObject refStone;
    public GameObject refGrass;
    public GameObject shop;
    private Vector2 shopPos = new Vector2(25, 25);
    void Start()
    {
        ListFillerAndAddingObjectsToTheScene.fillListAndAddToScene(ref grass, ref refGrass, countOfGrass, this.transform);
        ListFillerAndAddingObjectsToTheScene.fillListAndAddToScene(ref stone, ref refStone, countOfStone, this.transform);
    }


}
