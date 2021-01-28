using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{


    [SerializeField] private GameObject ground;
    private UnityEngine.Tilemaps.Tilemap tilemap;
    
    // Start is called before the first frame update
    void Start()
    {

       tilemap = ground.GetComponent<UnityEngine.Tilemaps.Tilemap>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Vector2 position = collider.gameObject.transform.position;
        Vector3Int colliderGridPosition = tilemap.WorldToCell(position);


        // Sprite tileSprite= tilemap.GetSprite(colliderGridPosition);



        Debug.Log($"walking on" + colliderGridPosition.ToString());



    }
}
