using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPosController : MonoBehaviour
{
    private const int mapSizeX = 100;
    private const int mapSizeZ = 50;
    public static Vector3 RandObjPos(float excludeX, float excludeZ, int mapX, int mapZ)
    {
        int posX = Random.Range(0, mapX);
        int posZ = Random.Range(0, mapZ);
        return new Vector3(posX, 0, posZ);
    }
    public static Vector3 RandObjPos(float excludeX, float excludeZ)
    {
        int posX = Random.Range(0, mapSizeX);
        int posZ = Random.Range(0, mapSizeZ);
        posX = (posX == excludeX && posZ == excludeX) ? posX += 10 : posX;
        return new Vector3(posX, 0, posZ);
    }

    public static Vector3 RandObjPos()
    {
        int posX = Random.Range(0, mapSizeX);
        int posZ = Random.Range(0, mapSizeZ);
        return new Vector3(posX, 0, posZ);
    }

}
