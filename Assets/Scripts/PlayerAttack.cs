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
    [SerializeField] private float attackRadius = 0.5f;
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
    }

    public void HitTree() {
        Collider[] hitTrees = Physics.OverlapSphere(attackPoint.position, attackRadius, treeLayer);

        foreach (Collider tree in hitTrees) {
            Debug.Log("Hit" + tree.name);
            tree.GetComponent<Tree>()?.TakeDamage(axeDamage);
        }
    }

    private IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}