using UnityEngine;

public class Pistol : Gun
{
    protected override void OnEndShoot()
    {
    }

    protected override void OnShooting()
    {
    }

    protected override void OnStartShoot()
    {
        StartCoroutine(MuzzleFlash());
        PlaySFX(Random.Range(.95f, 1f), Random.Range(1f, 1.05f));
        DoRecoil();
    }
}
