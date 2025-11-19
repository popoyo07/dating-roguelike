using UnityEngine;

public class VSFanim : MonoBehaviour
{
    public SpriteRenderer enemySprite;

    private void Awake()
    {
        enemySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemySprite.enabled = false;

    }

    // This is the one the Animation Event will call
    public void OnBeingAttackedEnd()
    {
        enemySprite.enabled = false;
    }

    public void OnBeingAttackedEnd2()
    {
        enemySprite.enabled = false;
        Debug.Log("WHYYYYYYYYYYYYYY");
    }
}
