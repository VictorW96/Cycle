using Mirror;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class World : NetworkBehaviour
{ 
    public static World _instance;
    public static World Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<World>();
            }

            return _instance;
        }
    }


    [System.Serializable]
    public class WorldParameters
    {
        [SyncVar]
        public int width;
        [SyncVar]
        public int height;

    }
    [SyncVar]
    public WorldParameters worldParameters;
    public GameObject PlayerCamera;

    [SyncVar]
    private float seed;

   
    private UnityEngine.Grid tileGrid;
    private Grid overlayGrid;

    [SyncVar]
    public Vector3 gridCellSize;

    public Tile[] tileList;
    private Tilemap ground;
    private Tilemap buildings;

    public override void OnStartServer()
    {
        GetTilemaps();
        seed = 1000000 * Random.value;
        readWorldParameters();
        GenerateWorld();
        
    }

    public override void OnStartClient()
    {
        GetTilemaps();
        GenerateWorld();
        Instantiate(PlayerCamera);
    }

    private void GetTilemaps()
    {
        GameObject groundGO = gameObject.transform.Find("Ground").gameObject;
        GameObject buildingsGO = gameObject.transform.Find("Buildings").gameObject;

        ground = groundGO.GetComponent<Tilemap>();
        buildings = buildingsGO.GetComponent<Tilemap>();
    }

    private void readWorldParameters()
    {
        
        using (StreamReader r = new StreamReader("./Raw/WorldGeneration.json"))
        {
            string jsonParameters = r.ReadToEnd();
            worldParameters = JsonConvert.DeserializeObject<WorldParameters>(jsonParameters);
        }
    }

    private void GenerateWorld()
    {
        Random.InitState((int)seed);
        ground.ClearAllTiles();

        gridCellSize = ground.layoutGrid.cellSize;

        ground.size = new Vector3Int(worldParameters.width, worldParameters.height, 1);
        overlayGrid = new Grid(worldParameters.width, worldParameters.height, gridCellSize.x, new Vector3(0, 0, 0));
        for (int i = 0; i < worldParameters.width; i++)
        {
            for (int j = 0; j < worldParameters.height; j++)
            {
                int tileID = Random.Range(0, tileList.Length-1); // ToDo Change to tiles for World Generation
                overlayGrid.SetValue(i, j, tileID);
                ground.SetTile(new Vector3Int(i, j, 0), tileList[tileID]);
            }
        }
    }

    public bool WorldIsEmpty()
    {
        if (overlayGrid == null)
        {
            return true;
        }
        return false;
    }

    public int GetTileIDfromWorldPosition(Vector3 worldPosition)
    {
        Vector3 gridPosition = GetGridPointfromWorldPosition(worldPosition);

        return overlayGrid.GetValue((int) gridPosition.x, (int) gridPosition.y);
    }

    public Vector3 GetGridPointfromWorldPosition(Vector3 worldPosition)
    {
        float x = worldPosition.x / gridCellSize.x;
        float y = worldPosition.y / gridCellSize.y;

        return new Vector3(x, y, worldPosition.z);
    }

    public void SetBuilding(Vector3 position, int tileId)
    {
        Vector3Int buildingPosition = new Vector3Int((int)position.x, (int)position.y, 0);
        buildings.SetTile(buildingPosition , tileList[tileId]);
        overlayGrid.SetValue(buildingPosition.x,buildingPosition.y, tileId);
    }

}
