using CodeMonkey.HealthSystemCM;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IGetHealthSystem
    {
        private HealthSystem _healthSystem;
        private Animator _animator;
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private float _deathTime = 2f;

        private void Awake()
        {
            _healthSystem = new(_maxHealth);
            _healthSystem.OnDead += HealthSystem_OnDead;
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            _healthSystem.Damage(damage);
            if (_animator != null)
                _animator.SetTrigger("Hurt");
        }

        private void Die()
        {
            if (_animator != null)
                _animator.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, _deathTime);
            enabled = false;
        }

        public void HealthSystem_OnDead(object sender, System.EventArgs e)
        {
            Die();
        }

        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
    }
}