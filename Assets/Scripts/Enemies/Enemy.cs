using CodeMonkey.HealthSystemCM;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IGetHealthSystem
    {
        private HealthSystem _healthSystem;
        [SerializeField] private int _maxHealth = 100;
        private int _currentHealth;
        private Animator _animator;

        private void Awake()
        {
            _healthSystem = new(_maxHealth);
            _healthSystem.OnDead += HealthSystem_OnDead;
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            _healthSystem.Damage(damage);
            _animator.SetTrigger("Hurt");
        }

        private void Die()
        {
            _animator.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2f);
            enabled = false;
        }

        private void HealthSystem_OnDead(object sender, System.EventArgs e)
        {
            Die();
        }

        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
    }
}