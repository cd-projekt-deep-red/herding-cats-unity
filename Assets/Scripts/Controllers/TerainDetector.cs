using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerainDetector : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private Transform footposition;
    
   
    // Start is called before the first frame update
    void Start()
    {

        footposition = GetComponent<Transform>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3Int tileCoordinate = tilemap.WorldToCell(new Vector3 { x = footposition.position.x, y = footposition.position.y, z = 0f });
        Sprite currentTerainSprite = tilemap.GetSprite(tileCoordinate);

        if (currentTerainSprite == null)
        {
            Debug.Log("printingNull Sprite");
        }
        else
        {
            Debug.Log("walking on terain" + currentTerainSprite.name);
        }

    }

   /* void OnTriggerEnter2D(Collider2D collision)
    {
        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();

        Transform footPosition = GetComponent<Transform>();

        Vector2 closestPoint = collision.ClosestPoint(new Vector2 { x = footPosition.position.x, y = footPosition.position.y });
        Debug.Log(closestPoint.ToString());
        Vector3Int tileCoordinates = tilemap.WorldToCell(new Vector3 { x = closestPoint.x, y = closestPoint.y + 1 });
        Debug.Log(tilemap.CellToWorld(tileCoordinates));


        Sprite newsprite = tilemap.GetSprite(tileCoordinates);

        if(newsprite == null)
        {
            Debug.Log("printingNull Sprite");
        }
        else 
        {
            Debug.Log("walking on terain" + newsprite.name);
        }


    } */

   
}
