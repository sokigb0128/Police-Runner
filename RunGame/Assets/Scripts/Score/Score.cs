using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    private static int score = 0;

    void Start()
    {
        score = 0;
    }
    
    public void ScorePlus(int plus)
    {
        score +=plus;
    }

    public static int GetScore()
    {
        return score;
    }
}
