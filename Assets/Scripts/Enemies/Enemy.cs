using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SimpleHealth hp;
    GameObject battleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp =  gameObject.GetComponent<SimpleHealth>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

    }
}
