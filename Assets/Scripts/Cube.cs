using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int number;
    public TextMeshProUGUI text;
    public GameObject SelectCorners, Corners;
    public Material FinalMaterial;

    private void Start()
    {
        text.text = number.ToString();
    }

    public MeshRenderer CubeMesh;
    private void OnMouseUpAsButton()
    {
        DestroyCube();
    }

    public void DestroyCube()
    {
        Debug.Log("Clicked");
        //When clicked destroy object with animation
        GetComponent<Animator>().SetTrigger("DestroyCube");
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
        if (GetComponentInParent<CubeManager>().isDragging)
        {
            GetComponentInParent<CubeManager>().AddSwipeList(this);
            SelectCorners.SetActive(true);
        }
    }

    public void DestroyAnimationEnd()
    {
        gameObject.SetActive(false);
        if (GetComponentInParent<CubeManager>().CubesWillBeDestroyed.Contains(this))
        {//Yok edilmesi gerekenler listesinde
            //Doğru
            GetComponentInParent<CubeManager>().CubesWillBeDestroyed.Remove(this);
        }
        else
        {//Yol edilmesi gereknler listesnde değil
            //Yanlış
            //GetComponentInParent<CubeManager>().isWrongCubeDestroyed = true;
            GetComponentInParent<CubeManager>().gameManager.LoseLive();
        }
    }

    public void ColorIt()
    {
        CubeMesh.material = FinalMaterial;
        Corners.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
