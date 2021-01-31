using UnityEngine;
using UnityEngine.UI;

public class FoundryMenu : MonoBehaviour
{
    public static FoundryMenu Instance;

    //Resources
    public int MetalScrap = 20;

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
    [SerializeField] private Text resourcesText;

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
        //Resources
        resourcesText.text = $"metal scrap: {MetalScrap}";

        //Pistol buttons
        pistolAmmoText.text = $"pistol ammo ({PistolAmmo}/{MaxPistolAmmo})";
        refillPistolButtonText.text = $"refill ({(MaxPistolAmmo - PistolAmmo)} metal scrap)";
        refillPistolButton.interactable = (PistolAmmo < MaxPistolAmmo) && 
            (MaxPistolAmmo - PistolAmmo <= MetalScrap);
        fiveAmmoPistolButton.interactable = (PistolAmmo <= MaxPistolAmmo - 5) &&
            (MetalScrap >= 5);

        //Shotgun buttons
        shotgunAmmoText.text = $"shotgun ammo ({ShotgunAmmo}/{MaxShotgunAmmo})";
        refillShotgunButtonText.text = $"refill ({(MaxShotgunAmmo - ShotgunAmmo) * 3} metal scrap)";
        refillShotgunButton.interactable = ShotgunAmmo < MaxShotgunAmmo &&
            ((MaxShotgunAmmo - ShotgunAmmo) * 3 <= MetalScrap);
        fiveAmmoShotgunButton.interactable = ShotgunAmmo <= MaxShotgunAmmo - 5 && 
            (MetalScrap >= 15);

        //Cheatyness
        if (Input.GetKeyDown(KeyCode.Backspace) && Application.isEditor) MetalScrap += 10;
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

    public void ClickRefillPistolAmmo()
    {
        int newBullets = MaxPistolAmmo - PistolAmmo;
        MetalScrap -= newBullets;
        PistolAmmo = MaxPistolAmmo;
    }

    public void ClickFivePistolAmmo()
    {
        MetalScrap -= 5;
        PistolAmmo += 5;
    }

    public void ClickRefillShotgunAmmo()
    {
        int newBullets = MaxShotgunAmmo - ShotgunAmmo;
        MetalScrap -= newBullets * 3;
        ShotgunAmmo = MaxShotgunAmmo;
    }

    public void ClickFiveShotgunAmmo()
    {
        MetalScrap -= 15;
        ShotgunAmmo += 5;
    }
}
