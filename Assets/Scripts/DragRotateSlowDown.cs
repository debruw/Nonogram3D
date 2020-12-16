﻿using UnityEngine;
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

    void OnMouseDown()
    {
        isDragging = true;
    }

    void Update()
    {

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
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y")))
        {
            cubeManager.transform.Rotate(Camera.main.transform.up * theSpeed.x * rotationSpeed, Space.World);
        }
        else
        {
            cubeManager.transform.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
        }       
        
    }
}