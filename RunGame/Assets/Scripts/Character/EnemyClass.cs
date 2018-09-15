using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour {

    private GameObject enemyPrefab;
    private GameObject bulletPrefab;
    private Enemy_SoundCheck soundCheck;
    private AudioSource audio=null;
    static private AudioClip gunShot=null;
        bool firstTime = true;

    // Use this for initialization
    void Start () {
        audio = GetComponentInParent<AudioSource>();
        if (!gunShot)
        {
            gunShot = (AudioClip)Resources.Load("Sound/gun");
        }
        bulletPrefab = (GameObject)Resources.Load("EnemyBullet");
        bulletPrefab.GetComponent<Bullet>().ThisIsNotPlayerBullet();

        soundCheck = transform.parent.gameObject.GetComponent<Enemy_SoundCheck>();
    }
	
	// Update is called once per frames
	void Update () {
        //this.transform.position -= GameObject.Find("Stage").GetComponent<Stage>().GetMoveSpeed() + new Vector3(0.1f, 0, 0);
        var pos = this.transform.position;

        // プレハブからインスタンスを生成
        if (pos.x <= 8.6 && firstTime)
        {
            var newBullet = Instantiate(bulletPrefab, gameObject.transform.position - new Vector3(1, 0, 0), Quaternion.identity);
            newBullet.name = "Enemy's Bullet";
            newBullet.GetComponent<Bullet>().ThisIsNotPlayerBullet();
            newBullet.transform.localScale = new Vector3(-1,1,1);
            firstTime = false;
            
            audio.PlayOneShot(gunShot);
        }

        if (pos.x <= -20.0 || pos.x<=-10)
        {
            Destroy(gameObject);
        }
        this.transform.rotation = Quaternion.Euler(0,0,0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Bullet")
        {
            GameObject.Find("Player").GetComponent<Score>().ScorePlus(10);
            soundCheck.PlayHit();
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }
    }
}
