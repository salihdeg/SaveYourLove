using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        private int _currentHealth;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _animator.SetTrigger("Hurt");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _animator.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2f);
            enabled = false;
        }
    }
}