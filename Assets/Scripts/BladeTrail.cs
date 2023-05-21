using UnityEngine;

public class BladeTrail : MonoBehaviour
{
    private Vector3 Direction { get; set; }

    private Camera _mainCamera;

    private Collider _sliceCollider;
    private TrailRenderer _sliceTrail;

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private bool _slicing;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _sliceCollider = GetComponent<Collider>();
        _sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            StartSlice();
        } else if (Input.GetMouseButtonUp(0)) {
            StopSlice();
        } else if (_slicing) {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        var position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        _slicing = true;
        _sliceCollider.enabled = true;
        _sliceTrail.enabled = true;
        _sliceTrail.Clear();
    }

    private void StopSlice()
    {
        _slicing = false;
        _sliceCollider.enabled = false;
        _sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        var newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        var transform1 = transform;
        Direction = newPosition - transform1.position;

        var velocity = Direction.magnitude / Time.deltaTime;
        _sliceCollider.enabled = velocity > minSliceVelocity;

        transform1.position = newPosition;
    }

}