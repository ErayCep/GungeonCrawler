using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float distanceToMove = 6f;
    public bool shouldChasePlayer;
    public Animator animator;
    private int health = 100;

    public bool shouldRunaway;
    public float runawayRange = 5f;

    private Vector3 moveDirection;
    Rigidbody2D rb;
    public Transform target;
    public SpriteRenderer spriteRenderer;

    public GameObject[] deathSplatters;

    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    public bool canShoot;

    public float fireRate;
    private float fireCount;
    public Transform firePoint;
    public GameObject bullet;
    public float distanceToShoot = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
        }
    }

    private void Update()
    {
        if(spriteRenderer.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, target.position) <= distanceToMove && shouldChasePlayer)
            {
                moveDirection = target.position - transform.position;
            }
            else
            {
                if(shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        moveDirection = wanderDirection;

                        if(wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
                        }
                    }

                    if(pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if(pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }
            }

            if(shouldRunaway && Vector3.Distance(transform.position, target.position) <= runawayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }
            /*else
            {
                moveDirection = Vector3.zero;
            }*/

            moveDirection.Normalize();
            rb.velocity = moveDirection * moveSpeed;

            if (canShoot && Vector3.Distance(target.position, transform.position) <= distanceToShoot)
            {
                fireCount -= Time.deltaTime;

                if (fireCount <= 0)
                {
                    fireCount = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
            }
        }

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(target.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);

            int rand = Random.Range(0, deathSplatters.Length);
            int randRotation = Random.Range(0, 4);
            Instantiate(deathSplatters[rand], transform.position, Quaternion.Euler(0f, 0f, randRotation * 90f));
        }
    }
}
