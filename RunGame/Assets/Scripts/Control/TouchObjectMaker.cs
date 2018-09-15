using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObjectMaker : MonoBehaviour {
    TouchInfo touchInfo = null;
    GameObject touch=null;
	// Use this for initialization
	void Start () {
        touchInfo = GameObject.Find("TouchFinder").GetComponent<TouchInfo>();
	}
	
	// Update is called once per frame
	void Update () {
        if (touchInfo.GetNowStateName(touchInfo.GetTouch()) == "Began")
        {
            if (touch) { Destroy(touch); }
            touch = new GameObject();
            touch.name = "Touch";
            Vector3 touchScreenPosition = touchInfo.GetTouchPos();

            // 10.0fに深い意味は無い。画面に表示したいので適当な値を入れてカメラから離そうとしているだけ.
            touchScreenPosition.z = 10.0f;

            Camera gameCamera = Camera.main;
            Vector3 touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);

            touch.transform.position = touchWorldPosition;
            var collider = touch.AddComponent<BoxCollider2D>();
            var rigid = touch.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            collider.isTrigger = true;
        }
    }
}
