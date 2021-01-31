using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxController : MonoBehaviour
{
    public static PickupEvent OnPickUpMetalScrap = new PickupEvent();
    public static PickupEvent OnPickUpPistolAmmo = new PickupEvent();
    public static PickupEvent OnPickUpShotgunAmmo = new PickupEvent();

    private void OnTriggerEnter(Collider other)
    {
        transform.gameObject.SetActive(false);
        OnPickUpMetalScrap.Invoke(Random.Range(3, 6));

        if (Random.Range(0f, 1f) < .5f)
        {
            int pistolAmmo = Random.Range(2, 6);
            if (FoundryMenu.Instance.PistolAmmo + pistolAmmo > FoundryMenu.Instance.MaxPistolAmmo)
                pistolAmmo = FoundryMenu.Instance.MaxPistolAmmo - FoundryMenu.Instance.PistolAmmo;
            if (pistolAmmo > 0)
                OnPickUpPistolAmmo.Invoke(pistolAmmo);
        }

        if (Random.Range(0f, 1f) < .33f && FoundryMenu.Instance.UnlockedShotgun)
        {
            int shotgunAmmo = Random.Range(2, 6);
            if (FoundryMenu.Instance.ShotgunAmmo + shotgunAmmo > FoundryMenu.Instance.MaxShotgunAmmo)
                shotgunAmmo = FoundryMenu.Instance.MaxShotgunAmmo - FoundryMenu.Instance.ShotgunAmmo;
            if (shotgunAmmo > 0)
                OnPickUpShotgunAmmo.Invoke(shotgunAmmo);
        }

        GameObject.Find("Game Controller").GetComponent<GameController>().PickedUpBox();
    }
}

public class PickupEvent : UnityEvent<int> { }
