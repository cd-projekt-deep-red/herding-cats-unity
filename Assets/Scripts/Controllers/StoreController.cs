using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    Dictionary<StoreItems, int> storeLevel = new Dictionary<StoreItems, int>();
    [SerializeField] private GameState gameState;

    [SerializeField] private GameObject boxStorePrefab;
    [SerializeField] private GameObject store;

    // Start is called before the first frame update
    void Start()
    {
        //items store to start
        storeLevel.Add(StoreItems.Box, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when player gets close display buy options
    private void OnTriggerEnter2D(Collider2D collision)
    {
     //call any entry text

     //display any prefabs of items in the store
     foreach(KeyValuePair<StoreItems,int> keyValue in storeLevel)
        {
            switch (keyValue.Key)
            {
                case StoreItems.Box:
                    GameObject boxStoreItem = Instantiate(boxStorePrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
                    boxStoreItem.transform.SetParent(store.transform, false);
                    boxStoreItem.transform.position = new Vector3 { x = 0f, y = 0f, z = 0f }; //this will be adjusted to the correcct spot
                    break;
                default:
                    break;
            }
        }
     


    }





}

public enum StoreItems
{
Box,
BigBox,
Fence,
Food
}
