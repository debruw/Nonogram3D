using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<Cube> CubesWillBeDestroyed;
    public List<Cube> CubesWillBeColored;
    [HideInInspector]
    public bool isWrongCubeDestroyed;

    public bool isDragging;
    public List<Cube> SwipeList;
    public void AddSwipeList(Cube cb)
    {
        SwipeList.Add(cb);
    }

    public void ClearSwipeList()
    {
        foreach (Cube cube in SwipeList)
        {
            cube.SelectCorners.SetActive(false);
            cube.DestroyCube();
        }
    }

    public void ColorAllCubes()
    {
        foreach (Cube cube in CubesWillBeColored)
        {
            cube.ColorIt();
        }
    }

    public void CloseAllCubes()
    {
        foreach (Cube cube in CubesWillBeColored)
        {
            cube.enabled = false;
        }
    }
}
