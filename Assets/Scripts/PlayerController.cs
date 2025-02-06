using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Input Settings")]
    [SerializeField] private InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private Vector3 input;

    [Header("Movement Settings")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 360;

    private void OnEnable() {
        if (inputActionAsset != null) {
            moveAction = inputActionAsset.FindAction("Move");
            moveAction.Enable();
        }
    }

    private void OnDisable() {
        if (moveAction != null) {
            moveAction.Disable();
        }
    }

    void Update() {
        GatherInput(moveAction.ReadValue<Vector2>());
        Look();
    }

    void FixedUpdate() {
        Move();
    }

    void GatherInput(Vector2 moveInput) {
        input = new Vector3(moveInput.x, 0, moveInput.y);

        if (input.magnitude > 1f) {
            input.Normalize();
        }
    }

    void Look() {
        if (input != Vector3.zero) {
            var relative = (transform.position + input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    void Move() {
        rb.MovePosition(transform.position + (transform.forward * input.magnitude) * speed * Time.deltaTime);
    }
}