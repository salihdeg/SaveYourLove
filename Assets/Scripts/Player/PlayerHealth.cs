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
        public static bool isDead = false;

        private void Awake()
        {
            _healthSystem = new(_maxHealth);
            _healthSystem.OnDead += Die;
            _animator = GetComponentInChildren<Animator>();
        }
        private void Start()
        {
            _healthSystem.HealComplete();
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

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
            if (isDead) return;

            _animator.SetTrigger("Die");
            isDead = true;
            PlayerController.isStop = true;
            StartCoroutine(LoadScene());
        }

        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(3f);
            PlayerController.isStop = false;
            isDead = false;
            SceneChanger.RestartScene();
        }
    }
}

