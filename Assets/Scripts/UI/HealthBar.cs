using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class HealthBar : MonoBehaviour
{
    private int Health;
    private SimpleHealth avatar;

    public Slider healthBar;


   
    void Awake() 
    {
        avatar = this.gameObject.GetComponent<SimpleHealth>();
        healthBar.maxValue = avatar.maxHealth;
        healthBar.value = avatar.health;
    }

    public void UpdateHealth() 
    {
        if (avatar == null) // this one will get component is avatar is null 
        {
            avatar = this.gameObject.GetComponent<SimpleHealth>();

        }
        healthBar.value = avatar.health;
        Debug.Log(" Health bar value " + healthBar.value + " hp value " + avatar.health);

    }

    public void UpdateMaxHealth() 
    {
        if (avatar == null) // this one will get component is avatar is null 
        {
            avatar = this.gameObject.GetComponent<SimpleHealth>();

        }
        healthBar.maxValue = avatar.maxHealth;
        Debug.Log(healthBar.value + avatar.health);
    }
}
