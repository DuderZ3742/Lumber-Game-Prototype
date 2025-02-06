using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
    [Header("Input Settings")]
    [SerializeField] private InputActionAsset inputActionAsset;
    private InputAction attackAction;
    private bool attackPressed = false;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int axeDamage = 10;
    [SerializeField] private LayerMask treeLayer;

    [Header("Animation Settings")]
    private Animator animator;

    [Header("Cooldown Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool canAttack = true;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        if (inputActionAsset != null) {
            attackAction = inputActionAsset.FindAction("Attack");
            attackAction.Enable();
        }
    }

    private void OnDisable() {
        if (attackAction != null) {
            attackAction.Disable();
        }
    }

    private void Update() {
        attackPressed = attackAction.ReadValue<float>() > 0.5f;

        if (attackPressed && canAttack) {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }

        ShowRaycast();
    }

    public void HitTree() {
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, attackPoint.up, out hit, attackRange, treeLayer)) {
            Tree tree = hit.collider.GetComponent<Tree>();
            if (tree != null) {
                tree.TakeDamage(axeDamage);
            }
        }
    }

    private IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void ShowRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, attackPoint.up, out hit, attackRange, treeLayer)) {
            Debug.DrawLine(attackPoint.position, hit.point, Color.red);
        }
        else {
            Debug.DrawRay(attackPoint.position, attackPoint.up * attackRange, Color.green);
        }
    }
}