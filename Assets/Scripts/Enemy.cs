using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        // Check if currentPoint is null before using it

        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.6f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.6f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }
    public void TriggerDeath()
    {
        StartCoroutine(DieWithDelay());
    }

    private IEnumerator DieWithDelay()
    {
        // Play the death sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Wait for the audio clip to finish
        yield return new WaitForSeconds(audioSource.clip.length);

        // Then destroy the GameObject
        Destroy(gameObject);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    }
    
}
