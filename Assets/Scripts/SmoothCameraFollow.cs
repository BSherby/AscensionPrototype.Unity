using System;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 3f;
    private Vector3 _currentVelocity = Vector3.zero;

    [Header("Camera Position Offset")]
    public Vector3 offset = new Vector3(0, 5, -10);

    [Header("Free Look Settings")]
    public float rotationSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        FreeLookAround();

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void FreeLookAround()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.RotateAround(target.position, Vector3.up, horizontalRotation);
        transform.RotateAround(target.position, transform.right, -verticalRotation);

        offset = transform.position - target.position;
    }
}
