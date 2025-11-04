using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContaint : MonoBehaviour
{   
    public InputController inputController;
    public  SortController sortController;
    public void Init()
     {
        inputController.Init();
        sortController.Init();
     }
}
