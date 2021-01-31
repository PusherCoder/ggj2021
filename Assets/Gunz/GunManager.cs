using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunType ActiveGun;

    private float scrollPosition = 0;

    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Shotgun;

    private GunType[] guns;

    private void Update()
    {
        int numGuns = 1;
        if (FoundryMenu.Instance.UnlockedShotgun) numGuns++;
        if (guns == null || guns.Length != numGuns)
        {
            guns = new GunType[numGuns];

            int index = 0;
            guns[index++] = GunType.Pistol;
            if (FoundryMenu.Instance.UnlockedShotgun) guns[index++] = GunType.Shotgun;
        }

        scrollPosition += Input.mouseScrollDelta.y;


        if (Input.GetKeyDown(KeyCode.Alpha1)) scrollPosition = 0f;
        if (Input.GetKeyDown(KeyCode.Alpha2)) scrollPosition = 1f;
        if (Input.GetKeyDown(KeyCode.Alpha3)) scrollPosition = 2f;
        if (Input.GetKeyDown(KeyCode.Alpha4)) scrollPosition = 3f;

        if (scrollPosition < -.49) scrollPosition = numGuns - 1;
        if (scrollPosition > numGuns - 1 + .49) scrollPosition = 0;

        ActiveGun = guns[Mathf.Clamp(Mathf.RoundToInt(scrollPosition), 0, guns.Length - 1)];

        switch (ActiveGun)
        {
            case GunType.Pistol:
                Pistol.SetActive(true);
                Shotgun.SetActive(false);
                break;
            case GunType.Shotgun:
                Pistol.SetActive(false);
                Shotgun.SetActive(true);
                break;
        }
    }
}

public enum GunType { Pistol, Shotgun }
