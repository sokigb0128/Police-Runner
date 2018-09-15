using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameText : MonoBehaviour {
    
    private TextRender textRender;

    // Use this for initialization
    void Start()
    {
        textRender = GetComponent<TextRender>();
        if (gameObject.name == "Jump")
        {
            textRender.Draw("JUMP", 1);
        }
        else
        {
            textRender.Draw("SHOT", 1);
        }
        var pos = gameObject.transform.position;
        pos.z = -0.9f;
        gameObject.transform.position = pos;
    }
}

