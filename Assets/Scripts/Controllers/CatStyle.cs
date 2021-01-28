using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStyle : MonoBehaviour
{
    public CatBreed breedData;

    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private Animator animator;
    [SerializeField]private string breedName;
    private bool breedSet = false;

    void Update()
    {
   
    }

    private void OnBecameVisible()
    {
        setBreed();
        
    }

    // Set the breed variables
    public void setBreed()
    {
      // For Bugfixing:
      breedName = breedData.name;

      // Set the sprite of the breed
      spriteRenderer.sprite = breedData.variantSprite;

      // Set the material of the breed
      spriteRenderer.material = breedData.variantMaterial;

      animator.runtimeAnimatorController = breedData.variantAnimationController;

      // Set the colors of the material
      for(int i=0;  i < breedData.colors.Length; i++)
      {
        int colorSelection = i * 20;
        string colorName = "Color_" + colorSelection.ToString();
        spriteRenderer.material.SetColor(colorName, breedData.colors[i]);
      }
    }
}
