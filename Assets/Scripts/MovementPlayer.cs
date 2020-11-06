using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask collisionLayer;
    


    private Vector3 velocity = Vector3.zero;
    private bool IsGrounded;

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.05f, collisionLayer);

    }

    void Update()
    {
        StartCoroutine(IncreaseSpeed());
        var velocity = Time.deltaTime * speed * Vector3.right;
        transform.position += velocity;

        //rb.AddForce(Vector3.right * Time.deltaTime * 0.01f);

        //rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.right, ref velocity, .05f);

        //rb.AddForce(Vector3.right * speed);
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded)
            {
              
                rb.AddForce(Vector3.up * jumpForce);
                gameObject.GetComponent<Animator>().SetTrigger("Jump");
            }
        }

    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(1f);
        speed += (float)0.00001;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, 0.5f);
    }

}