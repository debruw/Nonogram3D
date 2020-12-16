using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer CubeMesh;
    public GameObject SelectCorners;

    private void Start()
    {
        colorStep = 1f / (float)GetComponentInParent<CubeManager>().CubesWillBeDestroyed.Count;
        StartColor = CubeMesh.material.color;
    }

    public void CheckAndDestroyCube()
    {
        //Tıklandığında objeyi kontrol et
        if (GetComponentInParent<CubeManager>().CubesWillBeDestroyed.Contains(this))
        {//Yok edilmesi gerekenler listesinde ise
         //Doğru
            GetComponent<Animator>().SetTrigger("DestroyCube");
        }
        else
        {//Yol edilmesi gereknler listesinde değil ise
            //Yanlış
            Debug.Log("Lose live");
            GetComponentInParent<CubeManager>().gameManager.LoseLive();
        }
    }

    private void OnMouseDown()
    {
        GetComponentInParent<CubeManager>().isDragging = true;
        GetComponentInParent<CubeManager>().AddSwipeList(this);
        SelectCorners.SetActive(true);
    }

    private void OnMouseUp()
    {
        GetComponentInParent<CubeManager>().ClearSwipeList();
        GetComponentInParent<CubeManager>().isDragging = false;        
    }

    private void OnMouseEnter()
    {
        if (GetComponentInParent<CubeManager>().isDragging && !GetComponentInParent<CubeManager>().SwipeList.Contains(this))
        {
            GetComponentInParent<CubeManager>().AddSwipeList(this);
            SelectCorners.SetActive(true);
        }
    }

    public void DestroyAnimationEnd()
    {
        GetComponentInParent<CubeManager>().CubesWillBeDestroyed.Remove(this);
        GetComponentInParent<CubeManager>().ColorAllCubes();
        gameObject.SetActive(false);
    }

    float colorStep;
    public Color FinalColor;
    Color StartColor;
    public void ColorIt()
    {
        CubeMesh.material.color = Color.Lerp(StartColor, FinalColor, colorStep);
        colorStep += colorStep;
    }
}
