using UnityEngine;

public class VSFanim : MonoBehaviour
{
    public SpriteRenderer enemySprite;

    private void Awake()
    {
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // This is the one the Animation Event will call
    public void OnBeingAttackedEnd()
    {
        enemySprite.enabled = false;
    }
}
