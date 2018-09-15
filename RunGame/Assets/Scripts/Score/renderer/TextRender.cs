using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRender : MonoBehaviour {
    
    public string fileName;
    static Sprite[] sprites = null;
    private List<GameObject> characterList = new List<GameObject>();



    // 数字をSpriteで描画する
    public void Draw(string name, float size)
    {
        if (name == null) return;
        
        int digit = name.Length;

        if (sprites == null)
        {
            //スプライトがなければ作成
            sprites = Resources.LoadAll<Sprite>(fileName);
        }

            List<Sprite> spriteList = new List<Sprite>();
            float spWidth=0;
            for (int nowDigit = 0; nowDigit < digit; nowDigit++)
            {
                //Loadしたspritesの中から文字を探す
                Sprite sprite = null;
                var sp_name = fileName + "_" + name[nowDigit];
                sprite = System.Array.Find(sprites, (s) => s.name.Equals(sp_name));
                spriteList.Add(sprite);
                spWidth += (sprite.bounds.size.x * size);
            }
            spWidth = spWidth / 2;


                //一桁ずつ
                for (int nowDigit = 0; nowDigit < digit; nowDigit++)
            {
                //ゲームオブジェクト生成
                var characterObj = new GameObject();
                characterObj.name = spriteList[nowDigit].name;
                characterObj.transform.SetParent(gameObject.transform);
                characterObj.transform.localScale = Vector3.one;

                //SpriteRendererクラスをAdd
                var spriteRenderer = characterObj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = spriteList[nowDigit];

                float spStartx = gameObject.transform.position.x - spWidth;
                if (nowDigit == 0)
                {
                    characterObj.transform.position = new Vector3(spStartx, gameObject.transform.position.y, 0);
                }
                else
                {
                    float prevCharaSize = characterList[nowDigit - 1].GetComponent<SpriteRenderer>().bounds.size.x * size;
                    characterObj.transform.position = new Vector3(characterList[nowDigit - 1].transform.position.x + prevCharaSize,
                        gameObject.transform.position.y, 0);
                }

                //画像のサイズ拡大縮小
                spriteRenderer.transform.localScale = new Vector3(size, size, 1.0f);

                characterList.Add(characterObj);
            }
    }

}
