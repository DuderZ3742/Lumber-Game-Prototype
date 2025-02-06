using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Input Settings")]
    [SerializeField] private InputActionAsset inputActionAsset;
    private InputAction moveAction;

    private Vector3 input;

    [Header("Movement Settings")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;

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
    }

    void FixedUpdate() {
        Move();
    }

    void GatherInput(Vector2 moveInput) {
        input = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
    }

    void Move() {
        var relative = (transform.position + input.ToIsometric()) - transform.position;
        rb.MovePosition(rb.position + relative * Time.fixedDeltaTime);
    }
}