using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCat : MonoBehaviour
{
  public CatBreed breedData;

  [SerializeField]private SpriteRenderer spriteRenderer;
  [SerializeField]private string breedName;
  private bool breedSet = false;

  void Update()
  {
      if(this.gameObject.activeSelf)
      {
        if(!breedSet)
        {
          setBreed();
          breedSet = true;
        }
      }
  }

  // Set the breed variables
  public void setBreed()
  {
    // For Bugfixing:
    breedName = breedData.name;

    // Set the material of the breed
    spriteRenderer.material = breedData.variantMaterial;

    spriteRenderer.material.SetFloat("_OutlineThickness", 0f);

    // Set the colors of the material
    for(int i=0;  i < breedData.colors.Length; i++)
    {
      int colorSelection = i * 20;
      string colorName = "Color_" + colorSelection.ToString();
      spriteRenderer.material.SetColor(colorName, breedData.colors[i]);
    }
  }
}
