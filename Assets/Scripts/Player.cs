using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public Animator animator;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 15;
    public SpriteRenderer spriteRenderer;
    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    public LayerMask groundLayer;
   

    private float temporaryJumpVelocity;
    private float jumpPadMultiplier = 1.75f; // Default jump pad multiplier
    private Animator jumpPadAnimator;
    public GameObject newProjectilePrefab; // Reference to the new projectile prefab
    public int maxNewBulletShots = 5; // Maximum shots for the new bullet type
    private int currentNewBulletShots; // Current remaining shots of the new bullet type
    public CarrotManager cm;
    public Transform gunTransform; // Reference to the gun's transform
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 21f; // Speed of the projectile
    public float projectileLifetime = 2f; // Lifetime of the projectile
    Controller2D controller;
    private bool isUsingNewBullet = false;

    private bool useTemporaryJump = false;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentNewBulletShots = maxNewBulletShots;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        temporaryJumpVelocity = jumpVelocity;

        GameObject jumpPad = GameObject.FindGameObjectWithTag("JumpPad");
        if (jumpPad != null)
        {
            jumpPadAnimator = jumpPad.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            if (useTemporaryJump)
            {
                velocity.y = temporaryJumpVelocity;
            }
            else
            {
                velocity.y = jumpVelocity;
            }

            animator.SetTrigger("Jump");
        }

        if (Mathf.Abs(input.x) > 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(input.x), 1);
        }
       

        if (Input.GetMouseButtonDown(0))
        {
            bool isIdle = (input.x == 0);

            animator.SetBool("IsMoving", !isIdle);
            animator.SetTrigger("Shoot");

            Vector2 bulletDirection = Vector2.right; // Default direction (right)

            if (transform.localScale.x < 0) // If the player is facing left, change the direction to left
            {
                
                bulletDirection = Vector2.left;
            }

            if (isUsingNewBullet && currentNewBulletShots > 0)
            {
                ShootNewBullet(bulletDirection);
                currentNewBulletShots--;
            }
            else
            {
                ShootOriginalBullet(bulletDirection);
            }
        }

      


        animator.SetBool("IsMoving", Mathf.Abs(input.x) > 0);
        animator.SetBool("IsFalling", velocity.y < 0 && controller.collisions.below == false);
        animator.SetBool("IsGrounded", controller.collisions.below == true);
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JumpPad"))
        {
            temporaryJumpVelocity = jumpVelocity * jumpPadMultiplier;

            if (jumpPadAnimator != null)
            {
                jumpPadAnimator.SetTrigger("TriggerDown");
            }

            useTemporaryJump = true;
        }

        if (other.CompareTag("Carrots"))
        {
            cm.carrotCount++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy");
        }

        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Picked up a pickup");
            isUsingNewBullet = true;
            currentNewBulletShots += 5;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("JumpPad"))
        {
            useTemporaryJump = false;
            temporaryJumpVelocity = jumpVelocity;

            if (jumpPadAnimator != null)
            {
                jumpPadAnimator.SetTrigger("TriggerUp");
            }
        }

        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Left a pickup");
            isUsingNewBullet = false;
        }
    }

    void ShootNewBullet(Vector2 direction)
    {
        Debug.Log("Shoot new bullet method called");

        if (gunTransform != null && newProjectilePrefab != null)
        {
            Vector3 spawnPosition = gunTransform.position;


            GameObject newProjectile = Instantiate(newProjectilePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("New projectile instantiated");

            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;
            newProjectile.transform.localScale = new Vector3(direction.x, 1, 1);

            Destroy(newProjectile, projectileLifetime);
        }
        else
        {
            Debug.LogWarning("gunTransform or newProjectilePrefab is null. Make sure they are assigned in the Inspector.");
        }
    }
    public void SwitchToNewBullet()
    {
        isUsingNewBullet = true;
    }
    void ShootOriginalBullet(Vector2 direction)
    {
        Debug.Log("Shoot original bullet method called");

        if (gunTransform != null)
        {
            Vector3 spawnPosition = gunTransform.position;

            GameObject originalProjectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Original projectile instantiated");

            Rigidbody2D rb = originalProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;

            Destroy(originalProjectile, projectileLifetime);
        }
        else
        {
            Debug.LogWarning("gunTransform is null. Make sure it is assigned in the Inspector.");
        }
    }
}
