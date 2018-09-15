using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    TouchInfo touchInfo;
    private bool isChangeOK = false;

    public void ChangeOK() {
        isChangeOK = true;
    }

    void Start()
    {
        touchInfo = GameObject.Find("TouchFinder").GetComponent<TouchInfo>();
    }
    
    // Update is called once per frame
    void Update () {
        if (touchInfo.GetNowStateName(touchInfo.GetTouch()) == "Began")
        {
            if (isChangeOK)
            {
                touchInfo.gameObject.name = "TouchFinder2";
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    public void ChangeTitle()
    {
        touchInfo.gameObject.name = "TouchFinder2";
        SceneManager.LoadScene("TitleScene");
    }

    public void ChangeGame()
    {
        touchInfo.gameObject.name = "TouchFinder2";
        SceneManager.LoadScene("GameScene");
    }
}
