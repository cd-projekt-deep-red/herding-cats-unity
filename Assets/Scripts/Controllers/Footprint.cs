using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public bool isWet = true;
    public FootprintType footprintType = FootprintType.Character;
    [SerializeField]private float fadeTime = 1f;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private Sprite[] footprintSprites;

    private float lifeLeft;

    void OnBecameVisible()
    {
      // Set the sprite based on the type of footprint
      if(isWet)
      {
        if(footprintType == FootprintType.Character)
        {
          spriteRenderer.sprite = footprintSprites[0];
        }
        else
        {
          spriteRenderer.sprite = footprintSprites[1];
        }
      }
      else
      {
        if(footprintType == FootprintType.Character)
        {
          spriteRenderer.sprite = footprintSprites[1];
        }
        else
        {
          spriteRenderer.sprite = footprintSprites[2];
        }
      }

      lifeLeft = fadeTime;
    }

    void Update()
    {
      if(lifeLeft > 0)
      {
        lifeLeft = lifeLeft - Time.deltaTime;
        // Update the opacity
        spriteRenderer.color = new Color(1f, 1f, 1f, lifeLeft/fadeTime);
      }
      else
      {
        // Destory the object
        Destroy(this.gameObject, 0f);
      }
    }

    public enum FootprintType
    {
      Character,
      Cat
    }
}
