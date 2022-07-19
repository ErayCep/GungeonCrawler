using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;
    Vector3 targetDirection;

    private void Start()
    {
        targetDirection = PlayerController.instance.transform.position - transform.position;
        targetDirection.Normalize();
    }

    private void Update()
    {
        transform.position += targetDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamageToPlayer();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
