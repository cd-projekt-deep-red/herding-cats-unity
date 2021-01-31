﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StoreController : MonoBehaviour
{
    Dictionary<StoreItems, int> storeLevel = new Dictionary<StoreItems, int>();
    public GameState gameState;

    [SerializeField] private GameObject boxStorePrefab;
    [SerializeField] private GameObject fenceStorePrefab;
    [SerializeField] private GameObject store;
    [SerializeField]private TextMeshProUGUI shopkeeperText;
    [SerializeField]private string[] shopkeeperDialog;
    [SerializeField]private AudioClip[] shopkeeperDialogSFX;
    [SerializeField]private AudioSource audioEmitter;
    [SerializeField]private Animator shopAnimatior;
    [SerializeField] private UIScript uiScript;
    [SerializeField] private GoalUI goalUIScript;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField]private AudioSource mainCameraSFXSource;
    [SerializeField]private AudioSource shopMusicSource;

    private bool newitems = true;
    private bool newItemsChatUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        //items store to start
        storeLevel.Add(StoreItems.Box, 1);




    }

    // Update is called once per frame
    void Update()
    {
        if (!spriteRenderer.isVisible)
        {

            if(newitems == true)
            {
                newitems = false;
                //update store prefabs
                foreach (KeyValuePair<StoreItems, int> keyValue in storeLevel)
                {
                    switch (keyValue.Key)
                    {
                        case StoreItems.Box:
                            GameObject boxStoreItem = Instantiate(boxStorePrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
                            boxStoreItem.transform.SetParent(store.transform, false);
                            boxStoreItem.transform.localPosition = new Vector3 { x = -2f, y = -1.5f, z = 0f }; //this will be adjusted to the correcct spot

                            break;
                        case StoreItems.Fence:
                            GameObject fenceStoreItem = Instantiate(fenceStorePrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
                            fenceStoreItem.transform.SetParent(store.transform, false);
                            fenceStoreItem.transform.localPosition = new Vector3 { x = -3.5f, y = -1.5f, z = 0f };
                            break;
                        default:
                            break;
                    }
                }

            }

        }
    }

    //when player gets close display buy options
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //call any entry text
        if (!newItemsChatUpdate)
        {
            hello();
        }
        else
        {
            helloNewItems();
        }

        goalUIScript.ToggleGoalDisplay(true);
        // Fade in Music
        shopMusicSource.volume = 0f;
        shopMusicSource.Play();
        shopMusicSource.DOFade(1f, 1f);
        mainCameraSFXSource.DOFade(0.25f, 1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        goalUIScript.ToggleGoalDisplay(false);
        shopMusicSource.DOFade(0f, 1f);
        mainCameraSFXSource.DOFade(0.75f, 1f);
    }

    public void itemPurchased(float cost)
    {
        gameState.playerMoney = gameState.playerMoney - cost;
        uiScript.updatePlayerMoney();

      shopAnimatior.SetBool("CanPurchase", true);
      shopAnimatior.SetTrigger("PurchaseMade");
    }

    public void notEnoughMoney()
    {
      shopAnimatior.SetBool("CanPurchase", false);
      shopAnimatior.SetTrigger("PurchaseMade");
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

        newItemsChatUpdate = false;
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
