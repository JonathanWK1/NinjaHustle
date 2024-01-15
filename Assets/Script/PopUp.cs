using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas canvas;
    public UnityEvent popUpShow;
    public UnityEvent popUpHide;
    [SerializeField]
    private Button button;
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        canvas.enabled = true;
        button?.Select();
        popUpShow.Invoke();
    }

    public void Hide()
    {
        canvas.enabled=false;
        popUpHide.Invoke();
    }
}
