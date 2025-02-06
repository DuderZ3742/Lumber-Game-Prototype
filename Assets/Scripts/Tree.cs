using UnityEngine;

public class Tree : MonoBehaviour {
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Health Bar Settings")]
    [SerializeField] private Healthbar healthbar;

    void Start() {
        currentHealth = maxHealth;

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
            DestroyTree();
        }
    }

    private void DestroyTree() {
        Destroy(healthbar.gameObject);
        Destroy(gameObject);
    }
}