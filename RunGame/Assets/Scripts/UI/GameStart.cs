using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

   private CharacterSelecter name0,name1,name2;
    public static string name="None";
    private TextRender textRender;
    private ChangeScene changer;
    private AudioSource audio;
    private FadeControl fade;
    static private AudioClip enter=null; 

    // Use this for initialization
    void Start()
    {
        audio =GetComponent<AudioSource>();
        if (!enter) { enter = (AudioClip)Resources.Load("Sound/enter"); }
        fade = GameObject.Find("Canvas").transform.Find("Image").GetComponent<FadeControl>();
        name0 = GameObject.Find("Name_0").GetComponentInChildren<CharacterSelecter>();
        name1 = GameObject.Find("Name_1").GetComponentInChildren<CharacterSelecter>();
        name2 = GameObject.Find("Name_2").GetComponentInChildren<CharacterSelecter>();
        textRender = GetComponent<TextRender>();
        changer = GetComponent<ChangeScene>();
        textRender.Draw("START", 1);
        fade.FadeIn();
    }

    private void Update()
    {
        //alpha値が1以上なら次のシーンへ
        if (fade.alphaScore >= 1.0f)
        {
            changer.ChangeGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        name = name0.GetCharacter() + name1.GetCharacter() + name2.GetCharacter();
        audio.PlayOneShot(enter);
        fade.FadeFlgOut();
    }
}
