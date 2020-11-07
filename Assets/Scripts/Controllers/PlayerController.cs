using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    
    [Header("Collisions")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private BoxCollider2D SlideCollider;
    
    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefabe;
    [SerializeField] private Transform LaunchOffset;
    [SerializeField] private float fireRate;
    [SerializeField] private float SecondBeforePistolMode;

    [Header("Speed")]
    [SerializeField] private float acceleration;
    public float speed;
    
    public Action<List<(string,int)>> onBonusUpdate;
    private bool IsGrounded;
    private Animator _animator;
    
    // FIRE
    private bool _isPistolMode;
    private bool _canFire;
    
    // DEATH
    private bool _isDead;
    public bool IsDead => _isDead;
    
    //BLOCK 

    private bool blocked = false;

    public bool Blocked
    {
        get => blocked;
        set => blocked = value;
    }


    //SLIDE
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
    
    //BONUS SPEED
    private float originalSpeed;
    private bool speedBonusActivated;
    private int speedBonusTimer;
    private float bonusSpeed;
    
    private static readonly int _Jump = Animator.StringToHash("Jump");
    private static readonly int _Slide = Animator.StringToHash("Slide");
    private static readonly int _PistolTime = Animator.StringToHash("PistolTime");
    private static readonly int _HasBeenHit = Animator.StringToHash("HasBeenHit");

    private void Awake()
    {
        originalJumpForce = jumpForce;
        originalScale = gameObject.transform.localScale;
        bonusScale = originalScale * 2;
        bonusSpeed = 0.7f;
        StartCoroutine(PistolMode());
        StartCoroutine(IncreaseSpeed());
        Collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        var position = GroundCheck.position;
        IsGrounded = Physics2D.OverlapCircle(position, 0.05f, collisionLayer)
                     || Physics2D.OverlapCircle(position, 0.05f, obstacleLayer);
    }

    void Update()
    {
        if (_isDead) return;
        
        var velocity = Time.deltaTime * speed * Vector3.right;
        transform.position += velocity;
            
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded && !Blocked)
            {

                rb.AddForce(Vector3.up * jumpForce);
                _animator.SetTrigger(_Jump);
            }
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!IsSlidding && IsGrounded)
            {
                IsSlidding = true;

                Collider.enabled = false;
                SlideCollider.enabled = true;

                _animator.SetTrigger(_Slide);
                StartCoroutine(ResetCollider());

            }
        }

        if (_isPistolMode && _canFire && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(HandleFireRate());
            if (projectilePrefabe.TryAcquire(out var projectile))
            {
                var projectileTransform = projectile.transform;
                projectileTransform.position = LaunchOffset.position;
                projectileTransform.rotation = LaunchOffset.rotation;
            }
        }
    }

    private IEnumerator HandleFireRate()
    {
        _canFire = false;
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            speed += acceleration;
        }
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
        _isPistolMode = true;
        _canFire = true;
        _animator.SetTrigger(_PistolTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, 0.05f);
    }

    public void Die()
    {
        _isDead = true;
        _animator.SetTrigger(_HasBeenHit);
    }
    
    public void randBonus()
    {
        int bonus = Random.Range(1, 4);
        switch (bonus)
        {
            case 1:
                Debug.Log("Bonus JUMP");
                speedBonusTimer = 0;
                giantBonusTimer = 0;
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
                speedBonusTimer = 0;
                jumpBonusTimer = 0;
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
            
            case 3:
                jumpBonusTimer = 0;
                giantBonusTimer = 0;
                originalSpeed = speed;
                if (speedBonusActivated)
                {
                    speedBonusTimer = 10;
                    break;
                }
                else
                {
                    speed += 0.1f; 
                }

                speedBonusActivated = true;
                speedBonusTimer = 10;
                StartCoroutine(UpdateSpeedBonus());
                updateBonusUI();
                break;
        }
    }

    private IEnumerator UpdateSpeedBonus()
    {
        while (speedBonusTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            speedBonusTimer -= 1;
            updateBonusUI();
        }

        speed = originalSpeed;
        speedBonusActivated = false;
        updateBonusUI();
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
        if(speedBonusActivated) to_send.Add(("SPEED",speedBonusTimer));
        onBonusUpdate(to_send);
    }
}