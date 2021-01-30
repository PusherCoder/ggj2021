using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10f;
    public float LookMult = 1f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform gunCameraTransform;
    [SerializeField] private LayerMask notPlayerLayerMask;
    private Rigidbody rigidBody;
    private NavMeshAgent navMeshAgent;

    private Vector3 moveVector = Vector3.zero;
    private float cameraPitch = 0f;
    private float cameraYaw = 0f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
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

        //Turn on navmesh agent
        navMeshAgent.enabled = true;

        //Movement
        navMeshAgent.Move(moveVector * Speed * Time.deltaTime);
    }
}
