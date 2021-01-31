using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunType ActiveGun;

    private float scrollPosition = 0;

    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Shotgun;

    private void Update()
    {
        scrollPosition += Input.mouseScrollDelta.y;

        if (scrollPosition < -.49) scrollPosition = 1;
        if (scrollPosition > 1.49) scrollPosition = 0;

        if (Input.GetKeyDown(KeyCode.Alpha1)) scrollPosition = 0f;
        if (Input.GetKeyDown(KeyCode.Alpha2)) scrollPosition = 1f;

        if (Mathf.RoundToInt(scrollPosition) == 0)
            ActiveGun = GunType.Pistol;
        else
            ActiveGun = GunType.Shotgun;

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
