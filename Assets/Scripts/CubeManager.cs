using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<Cube> CubesWillBeDestroyed;
    public List<Cube> CubesWillBeColored;

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
            cube.CheckAndDestroyCube();
        }
        SwipeList.Clear();
    }

    public GameObject FinalObject;
    public void ColorAllCubes()
    {
        foreach (Cube cube in CubesWillBeColored)
        {
            cube.ColorIt();
        }
        FinalObject.transform.DOMove(new Vector3(0, FinalObject.transform.position.y, 0), 1);
        FinalObject.transform.DORotate(new Vector3(0, 45, 0), 1);
    }

    public void CloseAllCubes()
    {
        foreach (Cube cube in CubesWillBeColored)
        {
            cube.enabled = false;
        }
    }
}
