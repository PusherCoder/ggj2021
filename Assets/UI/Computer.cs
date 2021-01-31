using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    private bool playerInTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            prompt.GetComponent<CanvasGroup>().alpha = 1f;
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("Game Controller").GetComponent<GameController>().ComputerAccess();
                prompt.GetComponent<Text>().text = "go find my cute lamb doll. i threw it away accidentally.";
            }
        }
        else
            prompt.GetComponent<CanvasGroup>().alpha = 0f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            playerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            playerInTrigger = false;
    }
}
