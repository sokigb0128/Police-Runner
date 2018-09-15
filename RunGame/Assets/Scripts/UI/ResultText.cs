using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultText : MonoBehaviour {
    
    private ChangeScene changer;
    private FadeControl fade;
    private bool touchText = false;
    private AudioSource audio;
    static private AudioClip select = null;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (!select)
        {
            select = (AudioClip)Resources.Load("Sound/select");
        }
        fade = GameObject.Find("Canvas").transform.Find("Image").GetComponent<FadeControl>();
        changer = GameObject.Find("SceneChanger").GetComponent<ChangeScene>();
        fade.FadeIn();
    }

    private void Update()
    {
        //alpha値が1以上なら次のシーンへ
        if (fade.alphaScore >= 1.0f && touchText)
        {
            if (gameObject.name == "Return to Title")
            {
                changer.ChangeTitle();
            }
            else
            {
                changer.ChangeGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        fade.FadeFlgOut();
        audio.PlayOneShot(select);
        touchText = true;
    }
}
