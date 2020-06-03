using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDUpdater : MonoBehaviour
{
    private Camera cam;
    private World world;
    [SerializeField] private TMP_Text tileNameText;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        world = World.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(updateOnMousePosition());
    }

    IEnumerator updateOnMousePosition()
    {
        if (world.WorldIsEmpty())
        {
            yield break;
        }

        Vector3 mousePosition = Input.mousePosition;
        Vector2 mousePos;

        mousePos.x = mousePosition.x;
        mousePos.y = mousePosition.y;

        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        int tileID = world.getTileIDfromWorldPosition(point);
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
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
}
