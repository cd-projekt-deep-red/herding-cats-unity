using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStyle : MonoBehaviour
{
    public CatBreed breedData;

    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private string breedName;
    private bool breedSet = false;

    void Update()
    {
      // If the breed is not yet set, set it
      if(!breedSet)
      {
        setBreed();
        breedSet = true;
      }
    }

    // Set the breed variables
    private void setBreed()
    {
      // For Bugfixing:
      breedName = breedData.name;

      // Set the sprite of the breed
      spriteRenderer.sprite = breedData.variantSprite;

      // Set the material of the breed
      spriteRenderer.material = breedData.variantMaterial;

      // Set the colors of the material
      for(int i=0;  i < breedData.colors.Length; i++)
      {
        int colorSelection = i * 20;
        string colorName = "Color_" + colorSelection.ToString();
        spriteRenderer.material.SetColor(colorName, breedData.colors[i]);
      }
    }
}
