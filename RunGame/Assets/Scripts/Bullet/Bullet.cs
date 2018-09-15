using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private bool isPlayerBullet = true;

    public void ThisIsNotPlayerBullet() {
        isPlayerBullet = false;
    }

    public bool IsPlayerBullet()
    {
        return isPlayerBullet;
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlayerBullet)
        {
            this.transform.position += new Vector3(0.2f, 0, 0);
        }
        else {

            this.transform.position -= GameObject.Find("Stage").GetComponent<Stage>().GetMoveSpeed() + new Vector3(0.15f, 0, 0);
        }

            var pos = this.transform.position;
        if (pos.x >= 7.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerBullet)
        {
            if (collision.tag == "Bullet")
            {
                if (!collision.GetComponent<Bullet>().IsPlayerBullet())
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
