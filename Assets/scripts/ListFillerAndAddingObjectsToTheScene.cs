
using System.Collections.Generic;
using UnityEngine;

public class ListFillerAndAddingObjectsToTheScene : MonoBehaviour
{
    public static void fillListAndAddToScene(ref List<GameObject> newList, ref GameObject myObj, int count, Transform parent, bool isActive = true)
    {
        for (int i = 0; i < count; i++)
        {
            newList.Add(myObj);
            var child = Instantiate(newList[i], parent);
            child.transform.position = RandPosController.RandObjPos();
            child.SetActive(isActive);
        }

    }
    public static void fillListAndAddToScene(ref List<GameObject> newList, ref GameObject myObj, int count, float mapSizeX, float mapSizeZ, Transform parent, bool isActive = true)
    {
        for (int i = 0; i < count; i++)
        {
            newList.Add(myObj);
            var child = Instantiate(newList[i], parent);
            child.transform.position = RandPosController.RandObjPos(mapSizeX, mapSizeZ);
            child.SetActive(isActive);
        }
    }




}
