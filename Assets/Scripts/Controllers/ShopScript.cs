﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI shopkeeperText;
    [SerializeField]private string[] shopkeeperDialog;
    [SerializeField]private AudioClip[] shopkeeperDialogSFX;
    [SerializeField]private AudioSource audioEmitter;
    [SerializeField]private Animator shopAnimatior;

    public void SetShopMessage(int messageIndex)
    {
      shopkeeperText.text = "    " + shopkeeperDialog[messageIndex] + "\n/";
      audioEmitter.PlayOneShot(shopkeeperDialogSFX[messageIndex]);
    }

    public void ResetPurchase()
    {
      shopAnimatior.ResetTrigger("PurchaseMade");
    }
}
