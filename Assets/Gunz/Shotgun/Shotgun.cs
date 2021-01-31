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
        if (timeSinceLastShot < .5f) return;

        lastShotTime = Time.time;
        
        if (Ammo < 1)
        {
            PlayEmptySFX(Random.Range(.95f, 1f), Random.Range(1.5f, 1.55f));
            return;
        }

        Ammo--;

        StartCoroutine(MuzzleFlash());
        PlaySFX(Random.Range(.95f, 1f), Random.Range(1f, 1.05f));
        DoRecoil();

        for (int i = 0; i < 12; i++)
        {
            Vector2 offset = Random.insideUnitCircle * .03f;
            GunRaycastHit hit = DoRaycast(offset.x, offset.y);
            if (hit.Hit)
            {
                if (i % 3 == 0)
                    SparksPsSpawner.OnSpawnSparksParticles.Invoke(hit.Position, hit.Normal);
                IDamagable damagable = hit.Object.GetComponent<IDamagable>();
                if (damagable != null) damagable.TakeDamage(15);
            }
        }
    }

    protected override string GetGunString()
    {
        return $"Shotgun {Ammo}";
    }
}
