using UnityEngine;

public class Shotgun : Gun
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
        if (timeSinceLastShot < 1f) return;

        lastShotTime = Time.time;
        
        if (FoundryMenu.Instance.ShotgunAmmo < 1)
        {
            PlayEmptySFX(Random.Range(.95f, 1f), Random.Range(1.5f, 1.55f));
            return;
        }

        FoundryMenu.Instance.ShotgunAmmo--;

        StartCoroutine(MuzzleFlash());
        PlaySFX(Random.Range(.95f, 1f), Random.Range(1f, 1.05f));
        DoRecoil();

        int damage = FoundryMenu.Instance.UnlockedShotgunHeavySlug ? 30 : 15;
        int numShots = FoundryMenu.Instance.UnlockedShotgunSpreadShot ? 60 : 12;
        float spread = FoundryMenu.Instance.UnlockedShotgunSpreadShot ? .1f : .03f;

        for (int i = 0; i < numShots; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spread;
            GunRaycastHit hit = DoRaycast(offset.x, offset.y);
            if (hit.Hit)
            {
                if (i % 3 == 0)
                    SparksPsSpawner.OnSpawnSparksParticles.Invoke(hit.Position, hit.Normal);
                IDamagable damagable = hit.Object.GetComponent<IDamagable>();
                if (damagable != null) damagable.TakeDamage(damage);
            }
        }
    }

    protected override string GetGunString()
    {
        return $"Shotgun {FoundryMenu.Instance.ShotgunAmmo} / {FoundryMenu.Instance.MaxShotgunAmmo}";
    }
}
