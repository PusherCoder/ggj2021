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
        if (Ammo < 1)
        {
            PlayEmptySFX(Random.Range(.95f, 1f), Random.Range(1.5f, 1.55f));
            return;
        }

        Ammo--;

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
        return $"Pistol {Ammo}";
    }
}
