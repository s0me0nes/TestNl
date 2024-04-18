using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private const string IdleAnim = "isIdle";
    private const string WalkAnim = "isWalking";
    private const string BackWalkAnim = "isBackWalk";

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _backWalkSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private CubesManager _cubesManager;

    private Cube _cube;
    private Animator _animator;
    private Rigidbody _rb;
    private Transform _playerTransform;
    private bool _canTakeCube;

    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.velocity = transform.forward * _walkSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rb.velocity = -transform.forward * _backWalkSpeed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool(IdleAnim, false);
            _animator.SetBool(WalkAnim, true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _animator.SetBool(WalkAnim, false);
            _animator.SetBool(IdleAnim, true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetBool(BackWalkAnim, true);
            _animator.SetBool(IdleAnim, false);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _animator.SetBool(BackWalkAnim, false);
            _animator.SetBool(IdleAnim, true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _playerTransform.Rotate(0, -_rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _playerTransform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _cubesManager.SpawnCube();
        }

        if (Input.GetKeyDown(KeyCode.R) && _canTakeCube)
        {
            _cube.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            _cube = cube.GetComponent<Cube>();
            _canTakeCube = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            _canTakeCube = false;
        }
    }
}