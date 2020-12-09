using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float RotationSpeed = 5;
    public GameObject CubeManager;
    Quaternion startRotation;    

    private void Start()
    {
        startRotation = CubeManager.transform.rotation;
    }

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * RotationSpeed * Mathf.Deg2Rad;
        
        CubeManager.transform.RotateAround(CubeManager.transform.position, Vector3.right, rotY);
        CubeManager.transform.RotateAround(CubeManager.transform.position,Vector3.up, -rotX);
    }

    public void ResetRotation()
    {
        CubeManager.transform.rotation = startRotation;
    }
}
