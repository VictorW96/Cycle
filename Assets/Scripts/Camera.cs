using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static WorldGeneration.WorldParameters worldParameters;
    Camera thiscamera;
    Transform thistransform;

    private void Awake()
    {
        thiscamera = GetComponent<Camera>();
        thistransform = GetComponent<Transform>();
        thistransform.position = new Vector3((int)worldParameters.width / 2, (int)worldParameters.length / 2, -10);
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
