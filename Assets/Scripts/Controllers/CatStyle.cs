using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStyle : MonoBehaviour
{
    public CatBreed[] breedArray;
    [SerializeField]private string breedName;
    [SerializeField]private SpriteRenderer spriteRenderer;
    private Dictionary<string,CatBreed> breeds = new Dictionary<string, CatBreed>();

    void Start()
    {
      // Add breeds to dictionary
      for(int i=0; i < breedArray.Length; i++)
      {
        if(breedArray[i] != null)
        {
          breeds.Add(breedArray[i].name, breedArray[i]);
        }
      }

      // Set the sprite of the breed
      spriteRenderer.sprite = breeds[breedName].variantSprite;

      // Set the material of the breed
      spriteRenderer.material = breeds[breedName].variantMaterial;

      // Set the colors of the material
      for(int i=0;  i < breeds[breedName].colors.Length; i++)
      {
        int colorSelection = i * 20;
        string colorName = "Color_" + colorSelection.ToString();
        spriteRenderer.material.SetColor(colorName, breeds[breedName].colors[i]);
      }
    }
}
