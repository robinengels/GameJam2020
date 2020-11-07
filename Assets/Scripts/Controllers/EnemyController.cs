using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float acceleration;
    
    private bool _isMoving;
    private Transform _transform;
    private BoxCollider2D _collider;
    private static readonly int _Attack = Animator.StringToHash("Attack");
    private static readonly int _Shot = Animator.StringToHash("Shot");

    private void Awake()
    {
        _transform = transform;
        _isMoving = true;
        StartCoroutine(IncreaseSpeed());
        _collider = GetComponent<BoxCollider2D>();
    }
    
    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            speed += acceleration;
        }
    }

    private void Update()
    {
        if (!_isMoving) return;
        var movement = Time.deltaTime * speed * Vector3.right;
        _transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isMoving) return;
        if (playerLayer != (1 << other.gameObject.layer | playerLayer)) return;
        _isMoving = false;
        StartCoroutine(PlayAttackAnimation());
        StartCoroutine(GameOver());
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        _isMoving = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void Die()
    {
        animator.SetTrigger(_Shot);
        _collider.enabled = false;
        _isMoving = false;
        // No need to destroy, ReleaseOnOutOfFrame takes care of it
        // The enemy on the left doesn't have a ReleaseOnOutOfFrame script but it can't be shot
    }

    private IEnumerator GameOver()
    {
        var animationCycleLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(animationCycleLength);
        GameManager.Instance.GameOver();
    }

    private IEnumerator PlayAttackAnimation()
    {
        while (true)
        {
            animator.SetTrigger(_Attack);
            yield return new WaitForSeconds(3f);
        }
    }
}
