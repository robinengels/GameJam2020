using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    public float speed;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask collisionLayer;


    [SerializeField] private ProjectileController projectilePrefabe;
    [SerializeField] private Transform LaunchOffset;

    [SerializeField] private float SecondBeforePistolMode;

    private Vector3 velocity = Vector3.zero;
    private bool IsGrounded;
    private bool canFire;
    private void Awake()
    {
        StartCoroutine(PistolMode());
    }
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

        
        if (canFire && Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectilePrefabe, LaunchOffset.position, LaunchOffset.rotation);
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(1f);
        speed += (float)0.00001;

    }

    private IEnumerator PistolMode()
    {
        yield return new WaitForSeconds(SecondBeforePistolMode);
        canFire = true;
        gameObject.GetComponent<Animator>().SetTrigger("PistolTime");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, 0.05f);
    }

}