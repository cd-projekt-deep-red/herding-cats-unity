using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreController : MonoBehaviour
{
    Dictionary<StoreItems, int> storeLevel = new Dictionary<StoreItems, int>();
    public GameState gameState;

    [SerializeField] private GameObject boxStorePrefab;
    [SerializeField] private GameObject store;
    [SerializeField]private TextMeshProUGUI shopkeeperText;
    [SerializeField]private string[] shopkeeperDialog;
    [SerializeField]private AudioClip[] shopkeeperDialogSFX;
    [SerializeField]private AudioSource audioEmitter;
    [SerializeField]private Animator shopAnimatior;

    private bool newitems = false;
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
        if (!newitems)
        {
            hello();
        }
        else
        {
            helloNewItems();
        }
        //display any prefabs of items in the store
        foreach (KeyValuePair<StoreItems, int> keyValue in storeLevel)
        {
            switch (keyValue.Key)
            {
                case StoreItems.Box:
                    GameObject boxStoreItem = Instantiate(boxStorePrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
                    boxStoreItem.transform.SetParent(store.transform, false);
                    boxStoreItem.transform.localPosition = new Vector3 { x = -2f, y = -1.5f, z = 0f }; //this will be adjusted to the correcct spot

                    break;
                default:
                    break;
            }
        }
    }

    public void itemPurchased()
    {
      // shopAnimatior.SetBool("CanPurchase", true);
      // shopAnimatior.SetTrigger("PurchaseMade");
    }

    public void notEnoughMoney()
    {
      // shopAnimatior.SetBool("CanPurchase", false);
      // shopAnimatior.SetTrigger("PurchaseMade");
    }


    public void hello()
    {
      shopAnimatior.SetFloat("New Items", 0.0f);
      shopAnimatior.SetTrigger("Welcome");
    }

    public void helloNewItems()
    {
      shopAnimatior.SetFloat("New Items", 1.0f);
      shopAnimatior.SetTrigger("Welcome");

      newitems = false;
    }

    public void SetShopMessage(int messageIndex)
    {
      Debug.Log(messageIndex);
      shopkeeperText.text = "    " + shopkeeperDialog[messageIndex] + "\n/";
      audioEmitter.PlayOneShot(shopkeeperDialogSFX[messageIndex]);
    }

    public void ResetPurchase()
    {
      shopAnimatior.ResetTrigger("PurchaseMade");
    }

    public void ResetWelcome()
    {
      shopAnimatior.ResetTrigger("Welcome");
    }


}

public enum StoreItems
{
Box,
BigBox,
Fence,
Food
}
