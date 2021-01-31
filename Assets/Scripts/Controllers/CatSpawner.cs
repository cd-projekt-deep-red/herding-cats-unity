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
    [SerializeField] private int spawnAmount;
    private Dictionary<string,CatBreed> breeds = new Dictionary<string, CatBreed>();
    private Collider2D spawnArea;


    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<Collider2D>();
        SpawnCatsRandom(spawnAmount);

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


    public void SpawnCatsRandom(int howManyCats)
    {
        Bounds bounds = spawnArea.bounds;
        Vector2 center = bounds.center;
        for (int i = 0; i < howManyCats; i++)
        {

            float x = 0;
            float y = 0;
            do
            {
                x = Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
                y = Random.Range(center.y - bounds.extents.y, center.x + bounds.extents.y);
            } while (!spawnArea.OverlapPoint(new Vector2 { x = x, y = y }));


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
            cat.GetComponent<CatBehavior>().fondness = breedInfo.breedFondnessMult;
        }
    }
}
