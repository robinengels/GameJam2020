using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask collisionLayer;

    [SerializeField] private TMP_Text bonusText;


    private Vector3 velocity = Vector3.zero;
    private bool IsGrounded;
    private float originalJumpForce;

    private void Awake()
    {
        originalJumpForce = jumpForce;
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

    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(1f);
        speed += (float)0.00001;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, 0.05f);
    }

    public void randBonus()
    {
        int bonus = Random.Range(1, 3);
        switch (bonus)
        {
            case 1:
                jumpForce = 180;
                StartCoroutine(bonusJumpCoolDown());
                break;
            
            case 2:
                Debug.Log("Bonus 2");
                break;
                    
        }
    }

    private IEnumerator bonusJumpCoolDown()
    {
        onBonusChange("SUPER JUMP");
        yield return new WaitForSeconds(10f);
        jumpForce = originalJumpForce;
    }
}