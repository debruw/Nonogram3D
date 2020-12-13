using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int CanvasNumber1, CanvasNumber2;
    public GameObject Canvas1, Canvas2;
    public GameObject SelectCorners, Corners;
    public Material FinalMaterial;
    public Material WrongDestroyMaterial;
    public MeshRenderer CubeMesh;
    bool isWrongTile;

    private void Start()
    {
        //Canvas1.GetComponentInChildren<TextMeshProUGUI>().text = CanvasNumber1.ToString();
        //Canvas2.GetComponentInChildren<TextMeshProUGUI>().text = CanvasNumber2.ToString();
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
            //isWrongTile = true;
            //CubeMesh.material = WrongDestroyMaterial;
            Corners.gameObject.SetActive(false);
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
        gameObject.SetActive(false);
    }

    public void ColorIt()
    {
        if (!isWrongTile)
        {
            CubeMesh.material = FinalMaterial;
            Corners.SetActive(false);
            Canvas1.gameObject.SetActive(false);
            Canvas2.gameObject.SetActive(false);
        }
    }
}
