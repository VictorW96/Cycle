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
        if (Input.GetMouseButtonDown(0))
        { 
            if (World.Instance.WorldIsEmpty())
            {
                return;
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
            CmdBuildTown(point);
        }

    }

    [Command]
    private void CmdBuildTown(Vector3 point)
    {
        Vector3 gridPosition = World.Instance.GetGridPointfromWorldPosition(point);
        World.Instance.SetBuilding(gridPosition, 20); // hardcoded
        RpcBuildTown(gridPosition);

    }

    [ClientRpc]
    private void RpcBuildTown(Vector3 point)
    {
        World.Instance.SetBuilding(point, 20);// hardcoded
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);

        GameObject thiscamera = Instantiate(PlayerCamera);
        DontDestroyOnLoad(thiscamera);
        mainCamera = Camera.main;
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

}
