using UnityEngine;

public class Pistol : Gun
{
    private float lastShotTime = -999f;

    protected override void OnEndShoot()
    {
    }

    protected override void OnShooting()
    {
        Shoot();
    }

    protected override void OnStartShoot()
    {
        Shoot();
    }


    private void Shoot()
    {
        float timeSinceLastShot = Time.time - lastShotTime;
        if (timeSinceLastShot < .5f) return;

        lastShotTime = Time.time;

        if (FoundryMenu.Instance.PistolAmmo < 1)
        {
            PlayEmptySFX(Random.Range(.95f, 1f), Random.Range(1.5f, 1.55f));
            return;
        }

        FoundryMenu.Instance.PistolAmmo--;

        StartCoroutine(MuzzleFlash());
        PlaySFX(Random.Range(.95f, 1f), Random.Range(1f, 1.05f));
        DoRecoil();

        GunRaycastHit hit = DoRaycast();
        if (hit.Hit)
        {
            SparksPsSpawner.OnSpawnSparksParticles.Invoke(hit.Position, hit.Normal);
            IDamagable damagable = hit.Object.GetComponent<IDamagable>();
            if (damagable != null) damagable.TakeDamage(40);
        }
    }

    protected override string GetGunString()
    {
        return $"Pistol {FoundryMenu.Instance.PistolAmmo} / {FoundryMenu.Instance.MaxPistolAmmo}";
    }
}
