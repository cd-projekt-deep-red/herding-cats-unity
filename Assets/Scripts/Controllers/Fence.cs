using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : Holdable
{

    private SpriteRenderer spriteRender;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnPickUp()
    {
        
    }

    public override void OnPutDown()
    {
        spriteRender.enabled = false;
        
    }
}
