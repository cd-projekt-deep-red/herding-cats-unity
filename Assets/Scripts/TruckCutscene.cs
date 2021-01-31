﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckCutscene : MonoBehaviour
{
    [SerializeField]private SpriteMask spriteMask;
    [SerializeField]private Sprite[] maskSprites;
    [SerializeField]private CatBreed[] catBreeds;
    [SerializeField]private CutsceneCat[] cutsceneCats;
    [SerializeField]private GameObject catsWrapper;
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip audioClip;
    [SerializeField]private GameObject player;

    public void SetMaskSprite(int spriteIndex)
    {
      spriteMask.sprite = maskSprites[spriteIndex];
    }

    public void randomizeBreeds()
    {
      // Randomize all cutscene cats breeds
      for(int i=0; i < cutsceneCats.Length; i++)
      {
        cutsceneCats[i].breedData = catBreeds[Random.Range(0, catBreeds.Length-1)];
      }
    }

    public void RemoveCutsceneObjects()
    {
      Destroy(catsWrapper, 0f);
    }

    public void PlaySounds(float pitch)
    {
      audioSource.pitch = pitch;
      audioSource.PlayOneShot(audioClip);
    }

    public void SetPlayerPosition(int positionIndex)
    {
      Vector3 playerPosA = new Vector3(-3.993258f, 5.095146f, 0f);
      Vector3 playerPosB = new Vector3(0f, 0f, 0f);
      if(positionIndex == 0)
      {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.position = playerPosA;
      }
      else
      {
        player.transform.position = playerPosB;
        player.GetComponent<SpriteRenderer>().enabled = true;
      }
    }
}
