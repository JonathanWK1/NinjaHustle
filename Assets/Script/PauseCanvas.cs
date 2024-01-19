using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    bool Paused;
    PopUp popUp;
    void Start()
    {
        Paused = false;
        popUp = GetComponent<PopUp>();
        
    }


    public void SetPaused()
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0 : 1;
    }
}
