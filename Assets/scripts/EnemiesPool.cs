using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{

    private List<GameObject> skeletonList = new List<GameObject>();
    public GameObject refSceleton;
    public int countOfSceletons = 25;

    void Start()
    {
        ListFillerAndAddingObjectsToTheScene.fillListAndAddToScene(ref skeletonList, ref refSceleton, countOfSceletons, this.transform);
        
    }





}
