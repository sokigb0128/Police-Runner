using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private CharacterSelecter selecter;
    private AudioSource audio;
    static private AudioClip select=null;
    // Use this for initialization
    void Start()
    {
        audio = GetComponentInParent<AudioSource>();
        if (!select)
        {
            select = (AudioClip)Resources.Load("Sound/select");
        }
        selecter = transform.parent.transform.Find("Chara").GetComponent<CharacterSelecter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Touch") {
            selecter.hitArrow = gameObject;
            audio.PlayOneShot(select);
        }
    }
}
