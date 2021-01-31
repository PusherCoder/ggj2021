using UnityEngine;
using UnityEngine.UI;

public class FoundryMenu : MonoBehaviour
{
    public static FoundryMenu Instance;

    //Pistol
    public int PistolAmmo = 25;
    public int MaxPistolAmmo = 25;

    //Shotgun
    public int ShotgunAmmo = 15;
    public int MaxShotgunAmmo = 15;

    //UI
    [Header("Menus")]
    [SerializeField] private GameObject pistolMenu;
    [SerializeField] private GameObject shotgunMenu;

    [Header("Pistol")]
    [SerializeField] private Text pistolAmmoText;
    [SerializeField] private Button refillPistolButton;
    [SerializeField] private Text refillPistolButtonText;
    [SerializeField] private Button fiveAmmoPistolButton;

    [Header("Shotgun")]
    [SerializeField] private Text shotgunAmmoText;
    [SerializeField] private Button refillShotgunButton;
    [SerializeField] private Text refillShotgunButtonText;
    [SerializeField] private Button fiveAmmoShotgunButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Pistol buttons
        pistolAmmoText.text = $"pistol ammo ({PistolAmmo}/{MaxPistolAmmo})";
        refillPistolButton.interactable = PistolAmmo < MaxPistolAmmo;
        fiveAmmoPistolButton.interactable = PistolAmmo <= MaxPistolAmmo - 5;

        //Shotgun buttons
        shotgunAmmoText.text = $"shotgun ammo ({ShotgunAmmo}/{MaxShotgunAmmo})";
        refillShotgunButton.interactable = ShotgunAmmo < MaxShotgunAmmo;
        fiveAmmoShotgunButton.interactable = ShotgunAmmo <= MaxShotgunAmmo - 5;
    }

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
