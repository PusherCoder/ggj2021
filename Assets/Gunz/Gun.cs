using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Vector3 BobAmount;
    public Vector3 RecoilAmount;
    public float RecoilReturnMult = 3f;

    [SerializeField] protected Transform muzzleFlash;

    protected AudioSource audioSource;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Bob();

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation, 
            Quaternion.Euler(0f, 0f, 0f), 
            Time.deltaTime * RecoilReturnMult);

        if (Input.GetMouseButtonDown(0)) OnStartShoot();
        else if (Input.GetMouseButton(0)) OnShooting();
        else if (Input.GetMouseButtonUp(0)) OnEndShoot();
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

    protected IEnumerator MuzzleFlash(float duration = .05f)
    {
        muzzleFlash.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        muzzleFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        muzzleFlash.gameObject.SetActive(false);
    }

    protected void PlaySFX(float volume, float pitch)
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    protected void DoRecoil()
    {
        transform.localRotation = Quaternion.Euler(RecoilAmount * Random.Range(.9f, 1.1f));
    }
}
