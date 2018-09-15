using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRender : MonoBehaviour {
    public int maxDigit;
    public string fileName;
    Sprite[] sprites = null;
    private List<GameObject> numberList = new List<GameObject>();


    // 数字をSpriteで描画する
    public void Draw(int num, float size)
    {
        if (num <= -1) return;

        string digitStr = num.ToString();
        int digit = digitStr.Length;

        if (digit > maxDigit)
        {
            Debug.Log(num + "exceed max digit :" + maxDigit);
            return;
        }

        if (sprites == null)
        {
            //スプライトがなければ作成
            sprites = Resources.LoadAll<Sprite>(fileName);
            //一桁ずつ
            for (int nowDigit = 0; nowDigit < maxDigit; nowDigit++)
            {
                //Loadしたspritesの中からfilename_0を探す
                Sprite sprite = null;
                var sp0_name = fileName + "_" + "0";
                sprite = System.Array.Find(sprites, (s) => s.name.Equals(sp0_name));
                
                //ゲームオブジェクト生成
                var numberObj = new GameObject();
                numberObj.name = sprite.name;
                numberObj.transform.SetParent(gameObject.transform);
                numberObj.transform.localScale = Vector3.one;

                //SpriteRendererクラスをAdd
                var spriteRenderer = numberObj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;

                float spWidth = ((sprite.bounds.size.x*size) * maxDigit)/2;
                float spStartx = gameObject.transform.position.x - spWidth;
                numberObj.transform.position = new Vector3(spStartx + (nowDigit * (sprite.bounds.size.x * size)) , 
                    gameObject.transform.position.y, 0);

                //画像のサイズ拡大縮小
                spriteRenderer.transform.localScale = new Vector3(size, size, 1.0f);
                
                numberList.Add(numberObj);
            }
        }

        string text=null;
        for (int i = 0; i < maxDigit - digit;i++) {
            text += "0";
        }
        text += digitStr;
        
        for (int i = 0; i < maxDigit; i++)
        {
            Sprite numSprite = null;
            var sp_name = fileName + "_" + text[i].ToString();
            numSprite = System.Array.Find(sprites, (s) => s.name.Equals(sp_name));
            numberList[i].GetComponent<SpriteRenderer>().sprite = numSprite;//Spriteを変更
        }
    }
}
