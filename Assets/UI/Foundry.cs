using UnityEngine;

public class Foundry : MonoBehaviour
{
    [SerializeField] private CanvasGroup prompt;
    [SerializeField] private CanvasGroup menu;

    private bool playerInTrigger = false;
    public static bool FoundryMenuVisible = false;

    private void Update()
    {
        if (playerInTrigger)
        {
            prompt.alpha = 1f;
            if (Input.GetKeyDown(KeyCode.F))
                FoundryMenuVisible = !FoundryMenuVisible;
        }
        else
            prompt.alpha = 0f;

        if (FoundryMenuVisible)
        {
            menu.alpha = 1f;
            Cursor.visible = true;
            menu.interactable = true;
            menu.blocksRaycasts = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            menu.alpha = 0f;
            Cursor.visible = false;
            menu.interactable = false;
            menu.blocksRaycasts = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
