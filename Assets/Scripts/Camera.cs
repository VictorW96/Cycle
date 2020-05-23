using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private static GameController.WorldParameters worldParameters;


    Camera thiscamera;
    Transform thistransform;

    private void Start()
    {
        thiscamera = GetComponent<Camera>();
        Camera.worldParameters = GameController.worldParameters;
        Vector3 middle = new Vector3((int)worldParameters.width / 2, (int)worldParameters.length / 2, -10);
        thiscamera.transform.position = middle;
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    { 

        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");

        if (xAxisValue != 0 || yAxisValue != 0)
        {
            thiscamera.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0));
        }

        
    }
}
