using UnityEngine;
using System.Collections;

public class DragRotateSlowDown : MonoBehaviour
{

    private float rotationSpeed = 10.0F;
    private float lerpSpeed = 1.0F;

    private Vector3 theSpeed;
    private Vector3 avgSpeed;
    private bool isDragging = false;
    private Vector3 targetSpeedX;

    public GameObject cubeManager;
    //bool letMove;

    void OnMouseDown()
    {
        isDragging = true;
        //letMove = true;
    }

    //private void OnMouseUp()
    //{
    //    letMove = false;
    //}

    void Update()
    {
        if (!cubeManager.GetComponent<CubeManager>().gameManager.isGameStarted)
        {
            return;
        }
        else if (cubeManager.GetComponent<CubeManager>().gameManager.isGameEnd)
        {
            cubeManager.transform.eulerAngles += new Vector3(0, .25f, 0);
            return;
        }
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && isDragging)
        {
            theSpeed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F);
            avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime);
        }
        else
        {
            if (isDragging)
            {
                theSpeed = avgSpeed;
                isDragging = false;
            }
            float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
        }

        if (Mathf.Abs(theSpeed.x) > Mathf.Abs(theSpeed.y))
        {
            cubeManager.transform.Rotate(Camera.main.transform.up * theSpeed.x * rotationSpeed, Space.World);
        }
        else
        {
            cubeManager.transform.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
        }

#elif UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && isDragging)
            {
                theSpeed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F) / 5;
                avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime);
            }
            //else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //{
            //    isDragging = false;
            //}
        }
        else
        {
            if (isDragging)
            {
                theSpeed = avgSpeed;
                isDragging = false;
            }
            float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
        }

        //if (letMove)
        //{
            if (Mathf.Abs(theSpeed.x) > Mathf.Abs(theSpeed.y))
            {
                cubeManager.transform.Rotate(Camera.main.transform.up * theSpeed.x * rotationSpeed, Space.World);
            }
            else
            {
                cubeManager.transform.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
            }
        //}
#endif
    }
}