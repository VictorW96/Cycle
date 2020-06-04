using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDUpdater : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private TMP_Text tileNameText;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(updateOnMousePosition());
    }

    IEnumerator updateOnMousePosition()
    {
        if (World.Instance.WorldIsEmpty())
        {
            yield break;
        }
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Vector3 mousePosition = Input.mousePosition;
        Vector2 mousePos;

        mousePos.x = mousePosition.x;
        mousePos.y = mousePosition.y;

        Vector3 point = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        int tileID = World.Instance.GetTileIDfromWorldPosition(point);
        string displayName = TileInformation.tileDictionary[tileID];

        tileNameText.text = displayName;
    }

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = mainCamera.pixelHeight - currentEvent.mousePosition.y;

        point = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + mainCamera.pixelWidth + ":" + mainCamera.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
}
