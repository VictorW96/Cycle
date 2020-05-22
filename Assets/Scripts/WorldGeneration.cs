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

    private int width;
    private int length;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
        width = tilemap.size[0];
        length = tilemap.size[1];
        for (int i = 0; i <width; i++)
        {
            Debug.Log(i);
            for (int j = 0; i<length; i++)
            {
                Tile randomTile = tileList[Random.Range(0, tileList.Length)];
                tilemap.SetTile(new Vector3Int(i, j, 0), randomTile);
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
