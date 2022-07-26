using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float timeBetweenShots;
    private float timeSinceShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject projectile = Instantiate(bullet, shootPoint.position, shootPoint.rotation) as GameObject;
            }

            if (Input.GetMouseButton(0))
            {
                timeSinceShot -= Time.deltaTime;

                if (timeSinceShot <= 0)
                {
                    GameObject projectile2 = Instantiate(bullet, shootPoint.position, shootPoint.rotation) as GameObject;
                    timeSinceShot = timeBetweenShots;
                }
            }
        }
    }
}
