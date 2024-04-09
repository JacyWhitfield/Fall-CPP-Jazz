using UnityEngine;
using System.Collections;

public class EnemyTurret : MonoBehaviour
{
    public GameObject bulletPrefab;        // Reference to the bullet prefab
    public Transform spawnPointLeft;       // Left spawn point for the bullet
    public Transform spawnPointRight;      // Right spawn point for the bullet
    public float bulletSpeed = 5f;         // Speed of the bullet
    public float shootingInterval = 2f;    // Interval between shots
    public float detectionRange = 10f;     // Range at which the player is detected

    private Transform player;              // Reference to the player's transform
    private float timeSinceLastShot = 0f;  // Time since the last shot
    private bool isFacingRight = true;     // Check if the turret is facing right

    private Animator animator;             // Reference to the turret's animator
    private AudioSource audioSource;

    private void Start()
    {
        // Find the player GameObject by its tag (you can change this to a different method if needed)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the player is in range
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Update the time since the last shot
            timeSinceLastShot += Time.deltaTime;

            // Check if it's time to shoot
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f; // Reset the timer
            }
        }

        // Flip the turret based on the player's position
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip(true);
        }
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip(false);
        }
    }

    private void Shoot()
    {
        // Check if the fire point is assigned
        Transform spawnPoint = isFacingRight ? spawnPointRight : spawnPointLeft;

        if (spawnPoint != null)
        {
            // Instantiate a bullet at the spawn point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Calculate the bullet's direction towards the player
            Vector2 direction = (player.position - spawnPoint.position).normalized;
            rb.velocity = direction * bulletSpeed;

            // Destroy the bullet after a certain time (you can adjust this)
            Destroy(bullet, 2f);

            // Play the shoot animation
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
        }
    }

    public void TriggerDeath()
    {
        StartCoroutine(DieWithDelay());
    }

    private IEnumerator DieWithDelay()
    {
       
        if (audioSource != null)
        {
            audioSource.Play();
        }
       
      
        yield return new WaitForSeconds(audioSource.clip.length);

        
        Destroy(gameObject);
    }

    private void Flip(bool isFlipped)
    {
        isFacingRight = isFlipped;

        // Flip the turret's local rotation
        if (isFlipped)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
    }
}
