using UnityEngine;
using UnityEngine.UI;

public class FoundryMenu : MonoBehaviour
{
    public static FoundryMenu Instance;

    private const int PLUS_TEN_PISTOL_COST = 30;
    private const int HIGH_VELOCITY_ROUNDS_COST = 75;
    private const int EXPLOSIVE_ROUNDS_COST = 125;

    private const int BUY_SHOTGUN_COST = 20;
    private const int PLUS_FIVE_SHOTGUN_COST = 30;
    private const int HEAVY_SLUG_COST = 75;
    private const int SPREAD_SHOT_COST = 125;

    //Resources
    public int MetalScrap = 0;

    //Pistol
    public int PistolAmmo = 25;
    public int MaxPistolAmmo = 25;
    public bool UnlockedPistolHighVelocityRounds = false;
    public bool UnlockedPistolExplosiveRounds = false;

    //Shotgun
    public bool UnlockedShotgun = false;
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
    [SerializeField] private Button unlockedShotgunButton;
    [SerializeField] private Text unlockedShotgunText;
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
        BoxController.OnPickUpPistolAmmo.AddListener((int amount) => { PistolAmmo += amount; });
        BoxController.OnPickUpShotgunAmmo.AddListener((int amount) => { ShotgunAmmo += amount; });
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

        plusTenMaxPistolAmmoButton.interactable = MetalScrap >= PLUS_TEN_PISTOL_COST;

        pistolHighVelocityRoundsText.text = UnlockedPistolHighVelocityRounds ? 
            "purchased" : 
            $"{HIGH_VELOCITY_ROUNDS_COST} metal scrap";
        pistolHighVelocityRoundsButton.interactable = MetalScrap >= HIGH_VELOCITY_ROUNDS_COST && 
            UnlockedPistolHighVelocityRounds == false;

        pistolExplosiveRoundsText.text = UnlockedPistolExplosiveRounds ? 
            "purchased" : 
            $"{EXPLOSIVE_ROUNDS_COST} metal scrap";
        pistolExplosiveRoundsButton.interactable = MetalScrap >= EXPLOSIVE_ROUNDS_COST 
            && UnlockedPistolExplosiveRounds == false;

        //Shotgun buttons
        unlockedShotgunText.text = UnlockedShotgun ? "purchased" : $"{BUY_SHOTGUN_COST} metal scrap";
        unlockedShotgunButton.interactable = MetalScrap >= BUY_SHOTGUN_COST && UnlockedShotgun == false;

        shotgunAmmoText.text = $"shotgun ammo ({ShotgunAmmo}/{MaxShotgunAmmo})";

        refillShotgunButtonText.text = $"refill ({(MaxShotgunAmmo - ShotgunAmmo) * 3} metal scrap)";
        refillShotgunButton.interactable = ShotgunAmmo < MaxShotgunAmmo &&
            ((MaxShotgunAmmo - ShotgunAmmo) * 3 <= MetalScrap);

        fiveAmmoShotgunButton.interactable = ShotgunAmmo <= MaxShotgunAmmo - 5 && 
            (MetalScrap >= 15);

        plusFiveMaxShotgunAmmoButton.interactable = MetalScrap >= PLUS_FIVE_SHOTGUN_COST;

        shotgunHeavySlugText.text = UnlockedShotgunHeavySlug ? 
            "purchased" : 
            $"{HEAVY_SLUG_COST} metal scrap";
        shotgunHeavySlugButton.interactable = MetalScrap >= HEAVY_SLUG_COST && UnlockedShotgunHeavySlug == false;

        shotgunSpreadShotText.text = UnlockedShotgunSpreadShot ? 
            "purchased" : 
            $"{SPREAD_SHOT_COST} metal scrap";
        shotgunSpreadShotButton.interactable = MetalScrap >= SPREAD_SHOT_COST && UnlockedShotgunSpreadShot == false;

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
        MetalScrap -= PLUS_TEN_PISTOL_COST;
        MaxPistolAmmo += 10;
    }
    
    public void ClickPistolHighVelocityRounds()
    {
        MetalScrap -= HIGH_VELOCITY_ROUNDS_COST;
        UnlockedPistolHighVelocityRounds = true;
    }

    public void ClickPistolExplosiveRounds()
    {
        MetalScrap -= EXPLOSIVE_ROUNDS_COST;
        UnlockedPistolExplosiveRounds = true;
    }

    //Shotgun
    public void ClickUnlockShotgun()
    {
        MetalScrap -= BUY_SHOTGUN_COST;
        UnlockedShotgun = true;
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

    public void ClickPlusFiveMaxShotgunAmmo()
    {
        MetalScrap -= PLUS_FIVE_SHOTGUN_COST;
        MaxShotgunAmmo += 5;
    }

    public void ClickShotgunHeavySlug()
    {
        MetalScrap -= HEAVY_SLUG_COST;
        UnlockedShotgunHeavySlug = true;
    }

    public void ClickShotgunSpreadShot()
    {
        MetalScrap -= SPREAD_SHOT_COST;
        UnlockedShotgunSpreadShot = true;
    }
}
