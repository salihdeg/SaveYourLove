using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IGetHealthSystem
    {
        [SerializeField] private int _maxHealth;
        private HealthSystem _healthSystem;
        private Animator _animator;

        private void Awake()
        {
            _healthSystem = new(_maxHealth);
            _healthSystem.OnDead += Die;
            _animator = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(int damage)
        {
            if (PlayerController.isBlocking)
            {
                damage /= 5;
            }
            else
            {
                _animator.SetTrigger("Hurt");
            }

            _healthSystem.Damage(damage);
        }

        public void Die(object sender, System.EventArgs e)
        {
            _animator.SetTrigger("Die");
            Destroy(gameObject, 2.5f);
            this.enabled = false;
        }

        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
    }
}

