using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    SpriteState spriteState;

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.spriteState = spriteState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
