using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour {
    Rigidbody2D rb;
    TouchInfo touchInfo;
    bool isSky;
  
    private GameObject bulletPrefab;
    private AudioSource audio=null;
    private AudioClip gunShot = null;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        gunShot = (AudioClip)Resources.Load("Sound/gun");
        touchInfo = GameObject.Find("TouchFinder").GetComponent<TouchInfo>();
        bulletPrefab = (GameObject)Resources.Load("Bullet");
    }
	
	// Update is called once per frame
	void Update () {
        var pos = this.transform.position;

 //       //ジャンプ
        if (touchInfo.GetNowStateName(touchInfo.GetTouch()) == "Began" && !isSky) {
            if (touchInfo.GetTouchPos().x <= Screen.width / 2)
            {
                rb.AddForce(Vector2.up * 1300f);
                isSky = true;
            }
        }

        //発砲
        if (touchInfo.GetNowStateName(touchInfo.GetTouch()) == "Began")
        {
            if (touchInfo.GetTouchPos().x >= Screen.width / 2) {
                var newBullet = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                newBullet.name = "Bullet";
                audio.PlayOneShot(gunShot);
            }
        }

        //座標補正
        if (pos.x < -6.0)
        {
            pos.x += 0.05f;
        }
        else if (pos.x > -6.0)
        {
            pos.x -= 0.05f;
        }

        //落ちたら消える
        if (pos.y <= -5.0f || pos.x <= -9.5f)
        {
            Death();
        }
        

        transform.rotation = new Quaternion();
        transform.position = new Vector3(pos.x,pos.y,0);
	}

    void Death() {
        SceneManager.LoadScene("ResultScene");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            isSky = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Bullet>().IsPlayerBullet())
        {
            Death();
        }
    }

}
