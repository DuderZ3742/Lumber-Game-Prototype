using UnityEngine;
using UnityEngine.InputSystem;

public class RotateToMouseIsometric : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask targetLayer;

    private void Start() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }
    }

    private void Update() {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse() {
        if (mainCamera == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayer)) {
            Vector3 targetPos = hit.point;
            targetPos.y = transform.position.y;

            Vector3 direction = (targetPos - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}