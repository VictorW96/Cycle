using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Grid worldGrid;
    public GameObject playerCamera;


    // Start is called before the first frame update
    private void Awake()
    {
        Instantiate(worldGrid);
        Instantiate(playerCamera);
    }
}
