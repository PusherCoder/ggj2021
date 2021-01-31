using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    private bool playerInTrigger = false;

    string[] computerMessages = {
        "press 'f' to read your email.",
        "go find my cute lamb doll. i threw it away accidentally.",
        "would you be a dear and search the trash for my missing faucet?",
        "i'm lost without my favorite painting. please find it.",
        "eat at wendys"
    };

    // Start is called before the first frame update
    void Start()
    {
        prompt.GetComponent<Text>().text = computerMessages[GameObject.Find("Game Controller").GetComponent<GameController>().gameStage];
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            prompt.GetComponent<CanvasGroup>().alpha = 1f;
            if ((Input.GetKeyDown(KeyCode.F)) && (!GameObject.Find("Game Controller").GetComponent<GameController>().missionStarted))
            {
                GameObject.Find("Game Controller").GetComponent<GameController>().ComputerAccess();
                prompt.GetComponent<Text>().text = computerMessages[GameObject.Find("Game Controller").GetComponent<GameController>().gameStage];
            }
        }
        else
            prompt.GetComponent<CanvasGroup>().alpha = 0f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            playerInTrigger = true;
            if (!GameObject.Find("Game Controller").GetComponent<GameController>().missionStarted)
            {
                prompt.GetComponent<Text>().text = computerMessages[GameObject.Find("Game Controller").GetComponent<GameController>().gameStage];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            playerInTrigger = false;
    }
}
