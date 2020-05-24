using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Grid worldGrid;
    public Camera playerCamera;

    public class WorldParameters
    {
        public int width;
        public int length;

    }

    public static WorldParameters worldParameters;
    public static Vector3 gridCellSize;


    // Start is called before the first frame update
    private void Awake()
    {
        gridCellSize = worldGrid.cellSize;
        using (StreamReader r = new StreamReader("./Raw/WorldGeneration.json"))
            {
                string jsonParameters = r.ReadToEnd();
                worldParameters = JsonConvert.DeserializeObject<WorldParameters>(jsonParameters);
            }
        Instantiate(worldGrid);
    }
}
