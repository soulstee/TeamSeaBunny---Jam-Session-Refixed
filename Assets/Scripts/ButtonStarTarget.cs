using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStarTarget : MonoBehaviour
{
    bool isOverCurrently = false;

    public ButtonStarHighlight highlightScript;

    void Update()
    {
        if (!isOverCurrently && highlightScript.IsMouseOver())
        {
            isOverCurrently = true;
        }else if(isOverCurrently && !highlightScript.IsMouseOver()){
            isOverCurrently = false;
        }
    }
}
