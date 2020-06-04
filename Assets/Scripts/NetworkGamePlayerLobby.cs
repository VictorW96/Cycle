using Mirror;
using System;
using UnityEngine;

public class NetworkGamePlayerLobby : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";

    public GameObject PlayerCamera;
    private Camera mainCamera;

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    if (world.WorldIsEmpty())
        //    {
        //        yield break;
        //    }

        //    Vector3 mousePosition = Input.mousePosition;
        //    Vector2 mousePos;
    
        //    mousePos.x = mousePosition.x;
        //    mousePos.y = mousePosition.y;

        //    Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        //    int tileID = world.getTileIDfromWorldPosition(point);
        //    string displayName = TileInformation.tileDictionary[tileID];

    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);

    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Command]
    private void CmdChangeTile()
    {

    }

    public void AfterWorldGeneration()
    {
        Instantiate(PlayerCamera);
        mainCamera = Camera.main;

    }

}
