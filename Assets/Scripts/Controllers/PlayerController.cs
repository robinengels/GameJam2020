﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pooling;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
   
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private TMP_Text bonusText;

    [SerializeField] private GameObject projectilePrefabe;
    [SerializeField] private Transform LaunchOffset;

    [SerializeField] private float SecondBeforePistolMode;

    public float speed;
    public Action<List<(string,int)>> onBonusUpdate;
    private bool canFire;
    private bool IsGrounded;

    //SLIDE
    [SerializeField] private BoxCollider2D SlideCollider;
    private BoxCollider2D Collider;
    private bool IsSlidding;

    //BONUS JUMP

    private float originalJumpForce;
    private bool jumpBonusActivated = false;
    private int jumpBonusTimer;
    
    //BONUS GIANT

    private Vector3 originalScale;
    private Vector3 bonusScale;
    private bool giantBonusActivated;
    private int giantBonusTimer;

    private void Awake()
    {
        originalJumpForce = jumpForce;
        originalScale = gameObject.transform.localScale;
        bonusScale = originalScale * 2;
        StartCoroutine(PistolMode());
        Collider = gameObject.GetComponent<BoxCollider2D>();
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

       
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded)
            {

                rb.AddForce(Vector3.up * jumpForce);
                gameObject.GetComponent<Animator>().SetTrigger("Jump");
            }
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!IsSlidding && IsGrounded)
            {
                IsSlidding = true;

                Collider.enabled = false;
                SlideCollider.enabled = true;

                gameObject.GetComponent<Animator>().SetTrigger("Slide");
                StartCoroutine(ResetCollider());

            }
        }

        if (canFire && Input.GetButtonDown("Fire1"))
        {
            if (projectilePrefabe.TryAcquire(out var projectile))
            {
                var projectileTransform = projectile.transform;
                projectileTransform.position = LaunchOffset.position;
                projectileTransform.rotation = LaunchOffset.rotation;
            }
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(1f);
        speed += (float)0.00001;

    }

    private IEnumerator ResetCollider()
    {
        yield return new WaitForSeconds(0.4f);
        Collider.enabled = true;
        SlideCollider.enabled = false;
        IsSlidding = false;

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

    public void randBonus()
    {
        int bonus = Random.Range(1, 3);
        switch (bonus)
        {
            case 1:
                Debug.Log("Bonus JUMP");
                jumpForce = 180;
                if (jumpBonusActivated)
                {
                    jumpBonusTimer = 10;
                    break;
                }
                jumpBonusActivated = true;
                jumpBonusTimer = 10;
                StartCoroutine(UpdateJumpBonus());
                updateBonusUI();
                break;
            
            case 2:
                gameObject.transform.localScale = bonusScale;
                Physics2D.IgnoreLayerCollision(15,8,true);
                if (giantBonusActivated)
                {
                    giantBonusTimer = 10;
                    break;
                }
                giantBonusActivated = true;
                giantBonusTimer = 10;
                StartCoroutine(UpdateGiantBonus());
                updateBonusUI();
                break;
                    
        }
    }

    private IEnumerator UpdateJumpBonus()
    {
        while(jumpBonusTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            jumpBonusTimer -= 1;
            updateBonusUI();
        }

        jumpForce = originalJumpForce;
        jumpBonusActivated = false;
        updateBonusUI();
    }
    
    private IEnumerator UpdateGiantBonus()
    {
        while(giantBonusTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            giantBonusTimer -= 1;
            updateBonusUI();
        }

        gameObject.transform.localScale = originalScale;
        Physics2D.IgnoreLayerCollision(15,8,false);
        giantBonusActivated = false;
        updateBonusUI();
    }
    
   

    private void updateBonusUI()
    {
        List<(string,int)> to_send = new List<(string, int)>();
        if(jumpBonusActivated) to_send.Add(("SUPER JUMP",jumpBonusTimer));
        if(giantBonusActivated) to_send.Add(("GIANT",giantBonusTimer));
        onBonusUpdate(to_send);
    }
}