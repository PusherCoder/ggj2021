using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private CanvasGroup prompt;
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
            prompt.alpha = 1f;
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("Game Controller").GetComponent<GameController>().ComputerAccess();
            }
        }
        else
            prompt.alpha = 0f;

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
