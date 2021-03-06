﻿using UnityEngine;
using Pooling;

public class ProjectileController : MonoBehaviour
{
    
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private float additionalSpeed = 0.2f;
    [SerializeField] private LayerMask enemyLayer;
    
    private void Update()
    {
        var velocity = Time.deltaTime * speed * Vector3.right;
        transform.position += velocity;

        speed = GameObject.Find("Player").GetComponent<PlayerController>().speed + additionalSpeed;
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyLayer != (1 << other.gameObject.layer | enemyLayer)) return;
        if (other.TryGetComponent(out EnemyController enemy))
        {
            enemy.Die();
        }
        gameObject.TryRelease();
    }
}
