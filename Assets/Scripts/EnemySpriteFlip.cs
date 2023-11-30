using UnityEngine;

public class EnemySpriteFlip : MonoBehaviour
{
    private Transform enemyTransform; // Reference to the enemy's transform
    private bool isFacingRight = true; // Track the enemy's flip state

    private void Start()
    {
        enemyTransform = transform; // Get the reference to the enemy's transform
    }

    // Method to flip the enemy sprite
    public void FlipSprite(bool faceRight)
    {
        if (faceRight && !isFacingRight)
        {
            // Flip the sprite to face right
            enemyTransform.localScale = new Vector3(1f, 1f, 1f);
            isFacingRight = true;
        }
        else if (!faceRight && isFacingRight)
        {
            // Flip the sprite to face left
            enemyTransform.localScale = new Vector3(-1f, 1f, 1f);
            isFacingRight = false;
        }
    }
}