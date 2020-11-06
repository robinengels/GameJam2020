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
        private static readonly int _PlayerContact = Animator.StringToHash("PlayerContact");

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
            animator.SetTrigger(_PlayerContact);
            StartCoroutine(GameOver());
        }

        private IEnumerator GameOver()
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
            GameManager.Instance.GameOver();
        }
    }
}
