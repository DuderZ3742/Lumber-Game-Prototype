using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    [SerializeField] private Image healthBarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth) {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
