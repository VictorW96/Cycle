using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class WorldGeneration : MonoBehaviour
{
    private Tilemap tilemap;

    public Tile[] tileList;
    public static GameController.WorldParameters worldParameters;


    // Start is called before the first frame update
    void Start()
    {
        WorldGeneration.worldParameters = GameController.worldParameters;
        tilemap = GetComponent<Tilemap>();
        tilemap.ClearAllTiles();

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
