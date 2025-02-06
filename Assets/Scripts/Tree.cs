using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {
    [Header("Tree Settings")]
    [SerializeField] private TreeData treeData;
    private int maxHealth;
    private int currentHealth;

    private Rigidbody rb;
    [SerializeField] private float forceMagnitude = 100f;

    [Header("Health Bar Settings")]
    [SerializeField] private Healthbar healthbar;

    void Start() {
        maxHealth = Random.Range(treeData.maxTreeHealth, treeData.minTreeHealth);
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        healthbar.UpdateHealthBar(maxHealth, currentHealth);
        healthbar.gameObject.SetActive(false);
    }
    
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth < maxHealth && !healthbar.gameObject.activeSelf)
        {
            healthbar.gameObject.SetActive(true);
        }
        
        if (currentHealth <= 0) {
            StartCoroutine(TreeFelledRoutine());
        }
    }

    private IEnumerator TreeFelledRoutine() {
        rb.isKinematic = false;
        transform.SetParent(null);
        healthbar.gameObject.SetActive(false);

        RewardPlayer();

        yield return new WaitForSeconds(0.1f);

        ApplyForce();

        yield return new WaitForSeconds(5f);

        DestroyTree();
    }

    private void ApplyForce() {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null) {
            Vector3 direction = (transform.position - player.position).normalized;
            rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);
        }
    }

    private void RewardPlayer() {
        int rewardAmount = Random.Range(treeData.maxWoodReward, treeData.minWoodReward);
        UIManager.playerWoodAmount += rewardAmount;
    }

    private void DestroyTree() {
        Destroy(healthbar.gameObject);
        Destroy(gameObject);
    }
}