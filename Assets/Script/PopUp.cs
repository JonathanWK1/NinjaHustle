using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas canvas;
    public UnityEvent popUpShow;
    public UnityEvent popUpHide;
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        canvas.enabled = true;
        popUpShow.Invoke();
    }

    public void Hide()
    {
        canvas.enabled=false;
        popUpHide.Invoke();
    }
}
