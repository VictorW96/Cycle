using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private static GameController.WorldParameters worldParameters;
    private static Vector3 gridCellSize;

    private Vector3 worldMiddle;
    private float x_down_edge;
    private float x_up_edge;
    private float y_down_edge;
    private float y_up_edge;

    Camera thiscamera;

    private void Start()
    {
        thiscamera = GetComponent<Camera>();
        worldParameters = GameController.worldParameters;
        gridCellSize = GameController.gridCellSize;

        float xCor = (worldParameters.width * gridCellSize[0]) / 2;
        float yCor = (worldParameters.length * gridCellSize[1]) / 2;
        worldMiddle = new Vector3(xCor, yCor, -1);
        thiscamera.transform.position = worldMiddle;
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

    private void moveCamera()
    {

    }
}
