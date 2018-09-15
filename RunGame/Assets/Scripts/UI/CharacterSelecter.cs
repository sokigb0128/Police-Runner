using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour {

    public GameObject hitArrow = null;

    private int selecter = 0;
    private bool firstTime = true;
    private string[] ABC = null;
    private TextRender textRender;

    // Use this for initialization
    void Start ()
    {
        textRender = GetComponent<TextRender>();
        string Characters = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
        ABC = Characters.Split(','); 
	}
	
	// Update is called once per frame
	void Update () {

        if (firstTime) {
            textRender.Draw(ABC[selecter], 3);
            firstTime = false;
        }
        if (hitArrow)
        {
                if (hitArrow.name == "DownArrow")
                {
                    selecter -= 1; if (selecter < 0) { selecter = ABC.Length - 1; }
                }
                //右側
                if (hitArrow.name == "UpArrow")
                {
                    selecter += 1; if (selecter >= ABC.Length) { selecter = 0; }
                }

            Destroy(transform.GetChild(0).gameObject);
            textRender.Draw(ABC[selecter], 3);
            
            hitArrow = null;
        }

    }

    public string GetCharacter() {
        return ABC[selecter];
    }
}
