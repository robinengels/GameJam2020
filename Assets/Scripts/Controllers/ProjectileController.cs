using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    [SerializeField]
    private float speed =0.7f;
  
    private void Update()
    {
        StartCoroutine(DestroyAuto());
        var velocity = Time.deltaTime * speed * Vector3.right;
        transform.position += velocity;

        speed = GameObject.Find("Player").GetComponent<PlayerController>().speed + 0.2f;
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }

    private IEnumerator DestroyAuto()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
