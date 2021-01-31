using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Vector3 BobAmount;
    public Vector3 RecoilAmount;
    public float RecoilReturnMult = 3f;
    public int Ammo = 25;

    [SerializeField] protected Transform muzzleFlash;

    [SerializeField] protected AudioSource shootAudioSource;
    [SerializeField] protected AudioSource clickAudioSource;
    
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
        shootAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Bob();
        HUDText.GunString = GetGunString();

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation, 
            Quaternion.Euler(0f, 0f, 0f), 
            Time.deltaTime * RecoilReturnMult);

        if (Input.GetMouseButtonDown(0) && Foundry.FoundryMenuVisible == false) OnStartShoot();
        else if (Input.GetMouseButton(0) && Foundry.FoundryMenuVisible == false) OnShooting();
        else if (Input.GetMouseButtonUp(0) && Foundry.FoundryMenuVisible == false) OnEndShoot();
    }

    private void Bob()
    {
        transform.localPosition = initialPosition + new Vector3(
            Mathf.Cos(Time.time * 9f) * PlayerController.MoveMagnitude * BobAmount.x,
            Mathf.Sin(Time.time * 7f) * PlayerController.MoveMagnitude * BobAmount.y,
            Mathf.Cos(Time.time * 5f) * PlayerController.MoveMagnitude * BobAmount.z);
    }

    protected abstract void OnStartShoot();
    protected abstract void OnShooting();
    protected abstract void OnEndShoot();

    protected abstract string GetGunString();

    protected IEnumerator MuzzleFlash(float duration = .05f)
    {
        muzzleFlash.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        muzzleFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        muzzleFlash.gameObject.SetActive(false);
    }

    protected void PlaySFX(float volume, float pitch)
    {
        shootAudioSource.volume = volume;
        shootAudioSource.pitch = pitch;
        shootAudioSource.Play();
    }

    protected void PlayEmptySFX(float volume, float pitch)
    {
        clickAudioSource.volume = volume;
        clickAudioSource.pitch = pitch;
        clickAudioSource.Play();
    }

    protected void DoRecoil()
    {
        transform.localRotation = Quaternion.Euler(RecoilAmount * Random.Range(.9f, 1.1f));
    }

    protected GunRaycastHit DoRaycast(float xOffset = 0f, float yOffset = 0f)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + xOffset, 0.5f + yOffset, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 150f))
        {
            return new GunRaycastHit
            {
                Hit = true,
                Position = hit.point,
                Normal = hit.normal,
                Object = hit.collider.gameObject
            };
        }

        return new GunRaycastHit { Hit = false };
    }
}

public struct GunRaycastHit
{
    public bool Hit;
    public Vector3 Position;
    public Vector3 Normal;
    public GameObject Object;
}
