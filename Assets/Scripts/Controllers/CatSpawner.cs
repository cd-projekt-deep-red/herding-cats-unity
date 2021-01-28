using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour

{

    [SerializeField] private GameObject catPrefab;
    [SerializeField] private GameObject catWrapper;
    [SerializeField] private GameObject boundsColliderObject;
    
    
     private EdgeCollider2D boundsCollider;

     

 

    // Start is called before the first frame update
    void Start()
    {
        SpawnCatsRandom();
        boundsCollider = boundsColliderObject.GetComponent<EdgeCollider2D>();

        Debug.Log(boundsCollider.bounds.ToString());
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnCatsRandom()
    {
        GameObject cat = Instantiate(catPrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
        cat.transform.SetParent(catWrapper.transform, false);
        //place cat at random point

        // can add code here to spawn different types of cats
    }
}
