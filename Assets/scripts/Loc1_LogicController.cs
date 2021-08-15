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
    public GameObject loc2;
    private Vector2 shopPos = new Vector2(25, 25);

    private void Awake()
    {
        EventController.bossFight += disableLocation;
    }


    void Start()
    {
        ListFillerAndAddingObjectsToTheScene.fillListAndAddToScene(ref grass, ref refGrass, countOfGrass, this.transform);
        ListFillerAndAddingObjectsToTheScene.fillListAndAddToScene(ref stone, ref refStone, countOfStone, this.transform);
       // PersonController.singlton.gameRepeat += anableLocation;
    }

    private void disableLocation()
    {
        loc2.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void anableLocation()
    {
        loc2.SetActive(false);
        this.gameObject.SetActive(true);
    }

}
