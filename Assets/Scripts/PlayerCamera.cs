﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerCamera : MonoBehaviour
{
    public float panSpeed = 10f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 4f;

    public float minSize = 2;
    public float maxSize = 8;

    private static GameController.WorldParameters worldParameters;
    private static Vector3 gridCellSize;

    private Vector3 worldMiddle;
    private Vector2 panLimit;

    private Camera cameraComponent;
    private void Start()
    {
        worldParameters = GameController.worldParameters;
        gridCellSize = GameController.gridCellSize;
        cameraComponent = GetComponent<Camera>();

        panLimit = new Vector2(worldParameters.width*gridCellSize.x, worldParameters.length*gridCellSize.y);

        float xCor = (worldParameters.width * gridCellSize[0]) / 2;
        float yCor = (worldParameters.length * gridCellSize[1]) / 2;
        worldMiddle = new Vector3(xCor, yCor, -1);
        transform.position = worldMiddle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;

        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;

        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;

        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;

        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cameraComponent.orthographicSize -= scroll * scrollSpeed * 300f * Time.deltaTime; 

        pos.x = Mathf.Clamp(pos.x, -1, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -1, panLimit.y);
        cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize, minSize, maxSize);

        transform.position = pos;
        
        
    }

    private void moveCamera()
    {

    }
}
