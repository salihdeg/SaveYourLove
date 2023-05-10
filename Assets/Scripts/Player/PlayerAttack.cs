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

        private Animator _animator;
        private Rigidbody2D _rb;

        [HideInInspector] public bool isAttacking = false;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartCoroutine(CheckAttackInput());
        }

        private IEnumerator CheckAttackInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }


            yield return null;
            StartCoroutine(CheckAttackInput());
        }

        private void Attack()
        {
            // Play an attack anim
            isAttacking = true;
            _animator.SetTrigger(AnimationStates.isAttacking);
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
            // Detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
            // Damage them
        }
    }

}