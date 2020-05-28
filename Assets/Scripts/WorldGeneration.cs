using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using Mirror;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class WorldGeneration : NetworkBehaviour
{

    public class WorldParameters
    {
        public int width;
        public int height;

    }
    public static WorldParameters worldParameters;


    private UnityEngine.Grid tileGrid;
    private Grid overlayGrid;

    public static Vector3 gridCellSize;
    public Tile[] tileList;
    public Tilemap tilemap;

    public override void OnStartServer()
    {
        tileGrid = tilemap.layoutGrid;
        gridCellSize = tileGrid.cellSize;

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
        tilemap.size = new Vector3Int(worldParameters.width, worldParameters.height, 1);
        for (int i = 0; i < worldParameters.width; i++)
        {
            for (int j = 0; j < worldParameters.height; j++)
            {
                Tile randomTile = tileList[Random.Range(0, tileList.Length)];
                tilemap.SetTile(new Vector3Int(i, j, 0), randomTile);
            }
        }
        overlayGrid = new Grid(worldParameters.width, worldParameters.height, gridCellSize.x, new Vector3(0,0,0));
    }
   
}
