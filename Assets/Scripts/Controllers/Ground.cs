using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{


    [SerializeField] private GameObject ground;
    private UnityEngine.Tilemaps.Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {

        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0 ; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
   
