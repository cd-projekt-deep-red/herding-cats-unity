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

        boundsCollider = boundsColliderObject.GetComponent<EdgeCollider2D>();
        SpawnCatsRandom(200);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void SpawnCatsRandom(int howManyCats)
    {
        for (int i = 0; i < howManyCats; i++)
        {
            float x = Random.Range(-1 * boundsCollider.bounds.extents.x, boundsCollider.bounds.extents.x);

            float y = Random.Range(-1 * boundsCollider.bounds.extents.y, boundsCollider.bounds.extents.y);

            GameObject cat = Instantiate(catPrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
            cat.transform.SetParent(catWrapper.transform, false);
            //place cat at random point
            cat.transform.localPosition = new Vector3 { x = x, y = y, z = 0f };
            // can add code here to spawn different types of cats
        }
    }
}
