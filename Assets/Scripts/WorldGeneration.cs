using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using Newtonsoft.Json;
using System.IO;

public class WorldGeneration : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile[] tileList;

    public class WorldParameters
    {
        public int width;
        public int length;
    }

    public static WorldParameters worldParameters;


    private void readParameters()
    {
        using (StreamReader r = new StreamReader("./Raw/WorldGeneration.json"))
        {
            string jsonParameters = r.ReadToEnd();
            worldParameters = JsonConvert.DeserializeObject<WorldParameters>(jsonParameters);
        }
    }

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.ClearAllTiles();

        readParameters();
        tilemap.size = new Vector3Int(worldParameters.width, worldParameters.length, 1);
        for (int i = 0; i < worldParameters.width; i++)
        {
            Debug.Log(i);
            for (int j = 0; j< worldParameters.length; j++)
            {
                Tile randomTile = tileList[Random.Range(0, tileList.Length)];
                tilemap.SetTile(new Vector3Int(i,j, 0), randomTile);
            }
        }

        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
