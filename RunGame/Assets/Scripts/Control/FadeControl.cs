using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //透明度の変化スピード用
    const float fadeSpeed = 0.02f;
    //不透明度管理
    public float alphaScore;
    //フェード操作フラグ
    public bool fadeInFlg = true;
    //フェード操作用画像
    Image fadeImage;

    // Use this for initialization
    void Start()
    {
        //Canvas内のImageの情報を取得する
        fadeImage = GetComponent<Image>();
        //透明度取得
        alphaScore = fadeImage.color.a;
    }

    void Update()
    {
        if (fadeInFlg == true)
        {
            FadeIn();
        }

        if (fadeInFlg == false)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        //透明度下げる
        alphaScore -= fadeSpeed;
        FadeInLimit();
        fadeImage.color = new Color(0, 0, 0, alphaScore);

    }

    void FadeInLimit()
    {
        if (alphaScore < 0)
        {
            alphaScore = 0;
        }
    }

    public void FadeOut()
    {
        //透明度上げる
        alphaScore += fadeSpeed;
        FadeOutLimit();
        fadeImage.color = new Color(0, 0, 0, alphaScore);
    }

    void FadeOutLimit()
    {
        if (alphaScore > 1)
        {
            alphaScore = 1.0f;
        }
    }

    public void FadeFlgOut()
    {
        fadeInFlg = false;
    }
}

