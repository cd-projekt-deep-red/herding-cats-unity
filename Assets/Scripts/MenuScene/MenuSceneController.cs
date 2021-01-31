using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //potentially should setup gamestate here
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
        
    }
}
