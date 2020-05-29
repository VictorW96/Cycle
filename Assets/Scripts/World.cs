using Mirror;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class World : NetworkBehaviour
{
    public static event Action<NetworkConnection> worldGenerationDone;

    public static World _instance;
    public static World Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<World>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("WorldGrid");
                    _instance = container.AddComponent<World>();
                }
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

    private bool worldGenerated;

    [SyncVar]
    private float seed;

   
    private UnityEngine.Grid tileGrid;
    private Grid overlayGrid;

    [SyncVar]
    public Vector3 gridCellSize;

    public Tile[] tileList;
    private Tilemap tilemap;


    public override void OnStartServer()
    {
        seed = 1000000 * Random.value;
        readWorldParameters();
        GenerateWorld();
    }

    public override void OnStartClient()
    {
        GenerateWorld();
        Instantiate(PlayerCamera);
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
        tilemap = GetComponentInChildren<Tilemap>();
        tilemap.ClearAllTiles();

        gridCellSize = tilemap.layoutGrid.cellSize;

        tilemap.size = new Vector3Int(worldParameters.width, worldParameters.height, 1);
        overlayGrid = new Grid(worldParameters.width, worldParameters.height, gridCellSize.x, new Vector3(0, 0, 0));
        for (int i = 0; i < worldParameters.width; i++)
        {
            for (int j = 0; j < worldParameters.height; j++)
            {
                int tileID = Random.Range(0, tileList.Length);
                overlayGrid.SetValue(i, j, tileID);
                tilemap.SetTile(new Vector3Int(i, j, 0), tileList[tileID]);
            }
        }
    }

}
