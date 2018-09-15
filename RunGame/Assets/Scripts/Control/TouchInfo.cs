using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInfo : MonoBehaviour
{

    private Vector3 touchPos = Vector3.zero;

    private int None = -1;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        var duplication = GameObject.Find("TouchFinder2");
        if (duplication) {
            Destroy(duplication.gameObject);
        }
    }

    public int GetTouch()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0)) { return (int)TouchPhase.Began; }
            if (Input.GetMouseButton(0)) { return (int)TouchPhase.Moved; }
            if (Input.GetMouseButtonUp(0)) { return (int)TouchPhase.Ended; }

        }
        else
        {
            if (Input.touchCount > 0)
            {
                return (int)Input.GetTouch(0).phase;
            }
        }
        return None;
    }

    public Vector3 GetTouchPos()
    {
        if (Application.isEditor)
        {
            if (GetTouch() != None) { return Input.mousePosition; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                UnityEngine.Touch touch = Input.GetTouch(0);
                touchPos = touch.position;
                touchPos.z = 0;
                return touchPos;
            }
        }
        return Vector3.zero;
    }

    public string GetNowStateName(int state) {
        if (state == (int)TouchPhase.Began) { return "Began"; }
        if (state == (int)TouchPhase.Moved) { return "Moved"; }
        if (state == (int)TouchPhase.Ended) { return "Ended"; }
        return "None";
    }

}
