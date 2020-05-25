﻿using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerCycle : NetworkManager
{
    public override void OnStartServer()
    {
        NetworkServer.SpawnObjects();
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}