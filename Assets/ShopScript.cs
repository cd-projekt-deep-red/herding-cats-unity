using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI shopkeeperText;
    [SerializeField]private string[] shopkeeperDialog;

    public void SetShopMessage(int messageIndex)
    {
      shopkeeperText.text = "    " + shopkeeperDialog[messageIndex] + "\n/";
    }
}
