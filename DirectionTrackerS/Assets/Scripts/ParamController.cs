using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamController : MonoBehaviour
{
    private int mode = 1; //1:direction; 2:direction crack; 3: number; 4:number crack
    void Awake()
    {
        mode = 1;
    }

    public void ChangeMode()
    {
        mode += 1;
        if (mode >= 5)
            mode = 1;
    }

    public int GetMode()
    {
        return mode;
    }
}
