using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour {
    private const bool SCORE = true,NAME=false;
    private bool firstTime = true, renderType;
    private int score;
    private string name;
    private NumberRender numRender;
    private TextRender textRender;

    private LoadRank loadRank;

    private void Start()
    {
        loadRank = GetComponentInParent<LoadRank>();
        if (this.transform.parent.name == "RankingScores")
        {
            renderType = SCORE;
        }
        else {
            renderType = NAME;
        }
    }

    // Update is called once per frame
    void Update () {
        if (loadRank.IsSortComplete())
        {
            if (firstTime)
            {

                if (renderType == SCORE)
                {
                    numRender = GetComponent<NumberRender>();
                    numRender.maxDigit = 5;
                    var list = loadRank.GetScoreList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        //逆になってるので反転する
                        if ((i+1).ToString() == gameObject.name)
                        {
                            score = list[i];
                        }
                    }
                }
                else
                {
                    textRender = GetComponent<TextRender>();
                    var list = loadRank.GetNameList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        //逆になってるので反転する
                        if ((i+1).ToString() == gameObject.name)
                        {
                            name = list[i];
                        }
                    }
                }

                firstTime = false;
                if (renderType == SCORE)
                {
                    numRender.Draw(score, 1);
                }
                else
                {
                    textRender.Draw(name, 1);
                }
            }
        }
	}
}
