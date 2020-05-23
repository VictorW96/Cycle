using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Grid worldGrid;
    public GameObject playerCamera;

    public class WorldParameters
    {
        public int width;
        public int length;

    }

    public static WorldParameters worldParameters;


    // Start is called before the first frame update
    private void Awake()
    {
        using (StreamReader r = new StreamReader("./Raw/WorldGeneration.json"))
            {
                string jsonParameters = r.ReadToEnd();
                worldParameters = JsonConvert.DeserializeObject<WorldParameters>(jsonParameters);
            }
        Instantiate(worldGrid);
        Instantiate(playerCamera);
    }
}
