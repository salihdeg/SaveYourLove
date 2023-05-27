using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Transform _attackPoint2;
        [SerializeField] private float _attackRange = 0.5f;
        [SerializeField] private LayerMask _enemyLayer;

        [SerializeField] private int _attackDamage = 50;
        [SerializeField] private float _attackRate = 2f;
        private float _nextAttackTime = 0f;

        private Animator _animator;
        private Rigidbody2D _rb;

        [HideInInspector] public static bool isAttacking = false;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartCoroutine(CheckAttackInput());
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null)
                return;

            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
            Gizmos.DrawWireSphere(_attackPoint2.position, _attackRange);
        }

        private IEnumerator CheckAttackInput()
        {
            if(Time.time >= _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0) && PlayerJump.isGrounded)
                {
                    Attack();
                    _nextAttackTime = Time.time + 1f / _attackRate;
                }
            }

            yield return null;
            StartCoroutine(CheckAttackInput());
        }

        private void Attack()
        {
            // Play an attack anim
            isAttacking = true;
            _animator.SetTrigger(AnimationStates.isAttacking);
            //DrawCircleAndDamgeEnemies is calling as animation event
            _rb.velocity = new Vector2(0f, _rb.velocity.y);

        }

        public void DrawCircleAndDamageToEnemies()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
            Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(_attackPoint2.position, _attackRange, _enemyLayer);
            List<Collider2D> empty = new();

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_attackDamage);
                    empty.Add(hitEnemies[i]);
                }
            }

            for (int i = 0; i < hitEnemies2.Length; i++)
            {
                if (!empty.Contains(hitEnemies2[i]))
                {
                    if (hitEnemies2[i].TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage(_attackDamage);
                    }
                }
            }
        }
    }
}