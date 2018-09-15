using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    GameObject stagePrefab;
    bool firstTime=true;
    Vector3 speed = new Vector3(0.15f, 0, 0);

    // Use this for initialization
   int RandomStage() {
        var random = Random.value;
        int stageNo;

        if (random <= ((100/3)*0.01)) { stageNo = 0; }
        else if ((random <= ((100 / 3) * 0.01)*2)) { stageNo = 1; }
        else if ((random <= ((100 / 3) * 0.01)*3)) { stageNo = 2; }
        else
        {
            stageNo = -99;
        }
        return stageNo;
    }


    void Start () {
        int stageNo = -99;
        while (stageNo == -99) {
            stageNo = RandomStage();
        }
        string resourceName = "Stage" + stageNo.ToString();
       stagePrefab = (GameObject)Resources.Load(resourceName);
    }

    public Vector3 GetMoveSpeed() { return speed; }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position -= speed;
        var pos = this.transform.position;

        // プレハブからインスタンスを生成
        //ステージふやす
        if (pos.x <= 0 && firstTime) {
            var newStage = Instantiate(stagePrefab,new Vector3(37.0f,-2.668f,0), Quaternion.identity);
            newStage.name = "Stage";
            firstTime = false;
        }

        //後ろに行ったら消える
        if (pos.x <= -40.0f)
        {
            Destroy(gameObject);
        }
    }
    
}
