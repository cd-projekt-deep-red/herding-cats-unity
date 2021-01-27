using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//look into unity sprite asset libraries?

//this allows us to instance an asset manager object in the editor
[CreateAssetMenu(fileName = "AssetManager", menuName = "AssetManager")]

public class AssetManager : ScriptableObject
{

    //we allow the AssetManager to collect a list of sprites (adjustable in unity)
    [SerializeField] private Sprite[] sprites = new Sprite[1];


    // we are gonna store all these in a dictionary
    public Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
   



    //not sure if this is called every time we access or just when we originally call this class??
    public void OnEnable() // when the asset manager is created we will add all the sprites to a dictionary
    {


        for (int i = 0; i < sprites.Length; i++)
        {

            if (!spriteDict.ContainsKey(sprites[i].name)) //double check for conflicting key
            {
                spriteDict.Add(sprites[i].name, sprites[i]);
            }
            else { Debug.Log("duplicate sprite in Asset Manager (RTD)"); }

        }




    }

    public Sprite RetriveSprite(string spritename)
    {

        //this should be more roubust with try and catch at some point
        return spriteDict[spritename];
    }
}



