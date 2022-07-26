using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //shooting variables
/*    public GameObject bullet;
    public Transform shootPoint;
    public float timeBetweenShots;
    private float timeSinceShot;*/

    //movement variables
    public float moveSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D rb;
    public Transform gunArm;

    public List<Gun> availableGuns = new List<Gun>();
    private int activeGun;

    //dash variables
    private float activeMoveSpeed;
    public float dashLength = 0.5f, dashCooldown = 1f, dashSpeed = 15f;
    private float timesinceDash, dashCoolCounter;

    public bool canMove = true;

    private Camera cam;
    public Animator animator;

    public SpriteRenderer bodySR;

    private void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            rb.velocity = new Vector2(moveInput.x * activeMoveSpeed, moveInput.y * activeMoveSpeed);

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }

            if (moveInput != Vector2.zero)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

 /*           if (Input.GetMouseButtonDown(0))
            {
                GameObject projectile = Instantiate(bullet, shootPoint.position, gunArm.rotation) as GameObject;
            }

            if (Input.GetMouseButton(0))
            {
                timeSinceShot -= Time.deltaTime;

                if (timeSinceShot <= 0)
                {
                    GameObject projectile2 = Instantiate(bullet, shootPoint.position, gunArm.rotation) as GameObject;
                    timeSinceShot = timeBetweenShots;
                }
            }*/

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if(availableGuns.Count > 0)
                {
                    activeGun++;
                    if(activeGun >= availableGuns.Count)
                    {
                        activeGun = 0;
                    }

                    SwitchGun();
                }
                else
                {
                    Debug.LogError("There is no guns to switch");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (timesinceDash <= 0 && dashCoolCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    timesinceDash = dashLength;
                }

            }

            if (timesinceDash > 0)
            {
                timesinceDash -= Time.deltaTime;

                if (timesinceDash <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }

        if(PlayerHealthController.instance.currentHealth <= 0)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void SwitchGun()
    {
        foreach(Gun theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);
        }

        availableGuns[activeGun].gameObject.SetActive(true);
    }
}
