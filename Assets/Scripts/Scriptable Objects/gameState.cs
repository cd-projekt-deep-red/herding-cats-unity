using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameState", menuName = "GameState")]
public class GameState : ScriptableObject
{
    
    private float playerMoney { get; set; }

   


    public void catsEvacuated(IList<GameObject> cats)
    {
        foreach(GameObject cat in cats)
        {
            CatStyle catStyle = cat.GetComponent<CatStyle>();

            playerMoney = playerMoney +  catStyle.breedData.value;

        }
    }



}
