using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PopUp helpPopUp;
    [SerializeField]
    PopUp pausePopUp;

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            helpPopUp.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePopUp.Toggle();
        }

    }
}
