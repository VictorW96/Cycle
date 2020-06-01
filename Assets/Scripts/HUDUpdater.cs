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
        Vector3 point = cam.ScreenToWorldPoint(Input.mousePosition);
        int tileID = world.getTileIDfromWorldPosition(point);
        string displayName = TileInformation.tileDictionary[tileID];

        tileNameText.text = displayName;
    }
}
