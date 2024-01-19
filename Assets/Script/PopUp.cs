using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas canvas;
    public UnityEvent popUpShow;
    public UnityEvent popUpHide;
    [SerializeField]
    private Button button;

    GameManager gameManager;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        canvas = GetComponent<Canvas>();
        popUpHide.AddListener(gameManager.FocusText);
    }

    public void Show()
    {
        canvas.enabled = true;
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
            button.Select();
        }
        popUpShow.Invoke();
    }

    public void Hide()
    {
        canvas.enabled=false;
        popUpHide.Invoke();
    }

    public void Toggle()
    {
        if (canvas.enabled)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
