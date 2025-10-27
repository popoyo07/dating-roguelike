using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class HealthBar : MonoBehaviour
{
    private int Health;
    private SimpleHealth avatar;

    public Slider healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        avatar = this.gameObject.GetComponent<SimpleHealth>();
        healthBar.maxValue = avatar.maxHealth;
        healthBar.value = avatar.health;
    }

    public void UpdateHealth() // this one will get component is avatar is null 
    {
        if (avatar == null)
        {
            avatar = this.gameObject.GetComponent<SimpleHealth>();

        }
        healthBar.value = avatar.health;
        Debug.Log(" Health bar value " + healthBar.value + " hp value " + avatar.health);

    }

    public IEnumerator UpdateMaxHealth() // this will wait for avatar to be not null to run
    {
        yield return new WaitUntil(() => 
        avatar.gameObject.GetComponent<SimpleHealth>() != null);

       healthBar.maxValue = avatar.maxHealth;
        Debug.Log(healthBar.value + avatar.health);
    }
}
