using System;
using System.Collections;
using UnityEngine;

namespace IPL
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;

        private Transform _transform;
        private static readonly int _Attack = Animator.StringToHash("Attack");

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            var movement = Time.deltaTime * speed * Vector3.right;
            _transform.position += movement;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
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
