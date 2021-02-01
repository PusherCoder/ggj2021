using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCard : MonoBehaviour
{
    public Text TitleText;

    private bool inflate = true;
    private int slowItDown = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slowItDown-- == 0)
        {
            if (inflate)
            {
                if (TitleText.fontSize++ > 85) inflate = false;
            }
            else
            {
                if (TitleText.fontSize-- < 55) inflate = true;
            }
            slowItDown = 5;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Application.Quit();
        }

    }
}
