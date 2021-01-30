using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameState", menuName = "GameState")]
public class GameState : ScriptableObject
{

    public float playerMoney = 0f;


   


    public void catsEvacuated(IList<GameObject> cats)
    {
      
        foreach(GameObject cat in cats)
        {
            CatStyle catStyle = cat.GetComponent<CatStyle>();
           
            playerMoney = playerMoney +  catStyle.breedData.value;

        }
    }



}
