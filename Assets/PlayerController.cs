using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static float MoveMagnitude;

    public float Speed = 10f;
    public float LookMult = 1f;

    public AudioClip[] PlayerWalking;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform gunCameraTransform;
    [SerializeField] private LayerMask notPlayerLayerMask;
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;

    private Vector3 moveVector = Vector3.zero;
    private float cameraPitch = 0f;
    private float cameraYaw = 0f;
    private float audioClipTime = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Look()
    {
        cameraPitch -= Input.GetAxis("Mouse Y") * LookMult;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        cameraYaw += Input.GetAxis("Mouse X") * LookMult;

        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        gunCameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, cameraYaw, 0f);
    }

    private void Movement()
    {
        Vector3 newVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) newVector.z++;
        if (Input.GetKey(KeyCode.S)) newVector.z--;
        if (Input.GetKey(KeyCode.D)) newVector.x++;
        if (Input.GetKey(KeyCode.A)) newVector.x--;
        if (newVector.magnitude > 1) newVector.Normalize();
        newVector = Quaternion.Euler(0f, cameraYaw, 0f) * newVector;

        moveVector = Vector3.Lerp(moveVector, newVector, Time.deltaTime * 15f);
        MoveMagnitude = moveVector.magnitude;

        //Turn on navmesh agent
        navMeshAgent.enabled = true;

        //Movement
        navMeshAgent.Move(moveVector * Speed * Time.deltaTime);

        if ((newVector.magnitude > 0.5f) && (audioClipTime < Time.time))
        {
            audioSource.clip = PlayerWalking[Random.Range(0, PlayerWalking.Length)];
            audioClipTime = Time.time + Random.Range(.33f, .38f);
            audioSource.volume = Random.Range(.45f, .5f) * .5f;
            audioSource.pitch = Random.Range(1.5f, 1.6f);
            audioSource.Play();
        }
        else if (newVector.magnitude <= 0.5f)
        {
            audioClipTime = Time.time;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 10f);
        }
    }
}
