using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetScore : MonoBehaviour {
    
    NumberRender numberRender;
    int number = 0;
    public int size = 1;

    // Use this for initialization
    void Start() {
            numberRender = GetComponent<NumberRender>();
            numberRender.maxDigit = 5;
    }

    // Update is called once per frame
    void Update() {
        int score = Score.GetScore();
        if (score >= number)
        {
            numberRender.Draw(score, size);
            number++;
        }
    }
}
