using UnityEngine;
using UnityEngine.UI;

public class FoundryMenu : MonoBehaviour
{
    [SerializeField] private GameObject pistolMenu;
    [SerializeField] private GameObject shotgunMenu;

    [SerializeField] private Button refillPistolButton;
    [SerializeField] private Text refillPistolButtonText;

    [SerializeField] private Button refillShotgunButton;
    [SerializeField] private Text refillShotgunButtonText;

    public void ClickPistolMenu()
    {
        pistolMenu.SetActive(true);
        shotgunMenu.SetActive(false);
    }

    public void ClickShotgunMenu()
    {
        pistolMenu.SetActive(false);
        shotgunMenu.SetActive(true);
    }
}
