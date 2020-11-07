using System;
using System.Collections;
using UnityEngine;

namespace IPL
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float acceleration;
        
        private bool _isMoving;
        private Transform _transform;
        private static readonly int _Attack = Animator.StringToHash("Attack");

        private void Awake()
        {
            _transform = transform;
            _isMoving = true;
            StartCoroutine(IncreaseSpeed());
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

        private void OnDestroy()
        {
            StopAllCoroutines();
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
}
