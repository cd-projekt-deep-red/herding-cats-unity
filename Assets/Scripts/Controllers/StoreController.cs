using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] private StoreInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory.boxLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
