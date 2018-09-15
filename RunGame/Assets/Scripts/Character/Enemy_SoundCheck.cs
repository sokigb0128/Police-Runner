using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SoundCheck : MonoBehaviour {

    private GameObject child;
    private int count = 0;

    private AudioSource audio = null;
    static private AudioClip hitBullet=null;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (!hitBullet)
        {
            hitBullet = (AudioClip)Resources.Load("Sound/hit");
        }
        child = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (child == null) {
            count++;
            if (count > 100) { Destroy(gameObject); }


        }
	}

   public void PlayHit() {
        audio.PlayOneShot(hitBullet);
    }
}
