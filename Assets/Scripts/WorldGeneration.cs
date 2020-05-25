using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using Mirror;
using System.IO;
using Newtonsoft.Json;

public class WorldGeneration : NetworkBehaviour
{

    public class WorldParameters
    {
        public int width;
        public int length;

    }

    public static WorldParameters worldParameters;
    public Grid worldGrid;
    public static Vector3 gridCellSize;
    public Tile[] tileList;

    private Tilemap tilemap;


    public override void OnStartServer()
    {
        base.OnStartServer();

        tilemap = GetComponent<Tilemap>();
        worldGrid = tilemap.layoutGrid;
        gridCellSize = worldGrid.cellSize;

        tilemap.ClearAllTiles();
        readWorldParameters();
        generateWorld();
        
    }

    private void readWorldParameters()
    {
        
        using (StreamReader r = new StreamReader("./Raw/WorldGeneration.json"))
        {
            string jsonParameters = r.ReadToEnd();
            worldParameters = JsonConvert.DeserializeObject<WorldParameters>(jsonParameters);
        }
    }

    private void generateWorld()
    {
        tilemap.size = new Vector3Int(worldParameters.width, worldParameters.length, 1);
        for (int i = 0; i < worldParameters.width; i++)
        {
            Debug.Log(i);
            for (int j = 0; j < worldParameters.length; j++)
            {
                Tile randomTile = tileList[Random.Range(0, tileList.Length)];
                tilemap.SetTile(new Vector3Int(i, j, 0), randomTile);
            }
        }
    }

   
}
