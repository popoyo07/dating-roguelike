using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private int Health;
    private SimpleHealth avatar;

    public Slider healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        avatar = this.gameObject.GetComponent<SimpleHealth>();
        healthBar.maxValue = avatar.maxHealth;
        healthBar.value = avatar.health;
    }

    public void UpdateHealth()
    {
        healthBar.value = avatar.health;
        Debug.Log(healthBar.value + avatar.health);

    }

    public void UpdateMaxHealth()
    {
        healthBar.maxValue = avatar.maxHealth;
        Debug.Log(healthBar.value + avatar.health);
    }
}
