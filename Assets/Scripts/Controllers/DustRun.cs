using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustRun : MonoBehaviour
{
    public void Remove()
    {
      Destroy(this.gameObject, 0f);
    }
}
