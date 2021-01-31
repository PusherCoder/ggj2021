using UnityEngine;
using UnityEngine.UI;

public class FoundryMenu : MonoBehaviour
{
    public static FoundryMenu Instance;

    //Resources
    public int MetalScrap = 0;

    //Pistol
    public int PistolAmmo = 25;
    public int MaxPistolAmmo = 25;
    public bool UnlockedPistolHighVelocityRounds = false;
    public bool UnlockedPistolExplosiveRounds = false;

    //Shotgun
    public int ShotgunAmmo = 15;
    public int MaxShotgunAmmo = 15;
    public bool UnlockedShotgunHeavySlug = false;
    public bool UnlockedShotgunSpreadShot = false;

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
    [SerializeField] private Button plusTenMaxPistolAmmoButton;
    [SerializeField] private Button pistolHighVelocityRoundsButton;
    [SerializeField] private Text pistolHighVelocityRoundsText;
    [SerializeField] private Button pistolExplosiveRoundsButton;
    [SerializeField] private Text pistolExplosiveRoundsText;

    [Header("Shotgun")]
    [SerializeField] private Text shotgunAmmoText;
    [SerializeField] private Button refillShotgunButton;
    [SerializeField] private Text refillShotgunButtonText;
    [SerializeField] private Button fiveAmmoShotgunButton;
    [SerializeField] private Button plusFiveMaxShotgunAmmoButton;
    [SerializeField] private Button shotgunHeavySlugButton;
    [SerializeField] private Text shotgunHeavySlugText;
    [SerializeField] private Button shotgunSpreadShotButton;
    [SerializeField] private Text shotgunSpreadShotText;

    private void Awake()
    {
        Instance = this;
        BoxController.OnPickUpMetalScrap.AddListener((int amount) => { MetalScrap += amount; });
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

        plusTenMaxPistolAmmoButton.interactable = MetalScrap >= 50;

        pistolHighVelocityRoundsText.text = UnlockedPistolHighVelocityRounds ? "purchased" : "150 metal scrap";
        pistolHighVelocityRoundsButton.interactable = MetalScrap >= 150 && UnlockedPistolHighVelocityRounds == false;

        pistolExplosiveRoundsText.text = UnlockedPistolExplosiveRounds ? "purchased" : "250 metal scrap";
        pistolExplosiveRoundsButton.interactable = MetalScrap >= 250 && UnlockedPistolExplosiveRounds == false;

        //Shotgun buttons
        shotgunAmmoText.text = $"shotgun ammo ({ShotgunAmmo}/{MaxShotgunAmmo})";

        refillShotgunButtonText.text = $"refill ({(MaxShotgunAmmo - ShotgunAmmo) * 3} metal scrap)";
        refillShotgunButton.interactable = ShotgunAmmo < MaxShotgunAmmo &&
            ((MaxShotgunAmmo - ShotgunAmmo) * 3 <= MetalScrap);

        fiveAmmoShotgunButton.interactable = ShotgunAmmo <= MaxShotgunAmmo - 5 && 
            (MetalScrap >= 15);

        plusFiveMaxShotgunAmmoButton.interactable = MetalScrap >= 50;

        shotgunHeavySlugText.text = UnlockedShotgunHeavySlug ? "purchased" : "150 metal scrap";
        shotgunHeavySlugButton.interactable = MetalScrap >= 150 && UnlockedShotgunHeavySlug == false;

        shotgunSpreadShotText.text = UnlockedShotgunSpreadShot ? "purchased" : "250 metal scrap";
        shotgunSpreadShotButton.interactable = MetalScrap >= 250 && UnlockedShotgunSpreadShot == false;

        //Cheatyness
        if (Input.GetKeyDown(KeyCode.Backspace) && Application.isEditor) MetalScrap += 10;
    }

    //Switch menus
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

    //Pistol
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

    public void ClickPlusTenMaxPistolAmmo()
    {
        MetalScrap -= 50;
        MaxPistolAmmo += 10;
    }
    
    public void ClickPistolHighVelocityRounds()
    {
        MetalScrap -= 150;
        UnlockedPistolHighVelocityRounds = true;
    }

    public void ClickPistolExplosiveRounds()
    {
        MetalScrap -= 250;
        UnlockedPistolExplosiveRounds = true;
    }

    //Shotgun
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

    public void ClickPlusFiveMaxShotgunAmmo()
    {
        MetalScrap -= 50;
        MaxShotgunAmmo += 10;
    }

    public void ClickShotgunHeavySlug()
    {
        MetalScrap -= 150;
        UnlockedShotgunHeavySlug = true;
    }

    public void ClickShotgunSpreadShot()
    {
        MetalScrap -= 250;
        UnlockedShotgunSpreadShot = true;
    }
}
