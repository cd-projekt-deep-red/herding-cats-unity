using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour

{
    public CatBreed[] breedArray;

    [SerializeField] private Sprite bellyCat;

    [SerializeField] private GameObject catPrefab;
    [SerializeField] private GameObject catWrapper;
    [SerializeField] private GameObject boundsColliderObject;
    private Dictionary<string,CatBreed> breeds = new Dictionary<string, CatBreed>();
    private EdgeCollider2D boundsCollider;


    // Start is called before the first frame update
    void Start()
    {
        boundsCollider = boundsColliderObject.GetComponent<EdgeCollider2D>();
        SpawnCatsRandom(100);

        // Add breeds to breed dictionary
        for(int i=0; i < breedArray.Length; i++)
        {
          if(breedArray[i] != null)
          {
            breeds.Add(breedArray[i].name, breedArray[i]);
          }
        }
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
            // Set the breed of the cat to a random breed from the breed dictionary
            CatStyle catStyle = catPrefab.GetComponent<CatStyle>();

            // TEMPORARY CODE TO LIMIT BREEDS TO BELLYCAT
            CatBreed breedInfo = breedArray[(int)Random.Range(0f, breedArray.Length-1)];
            while(breedInfo.variantSprite != bellyCat)
            {
              breedInfo = breedArray[(int)Random.Range(0f, breedArray.Length-1)];
            }

            catStyle.breedData = breedInfo;

        }
    }
}
