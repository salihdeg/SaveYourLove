using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private int _attackDamage;

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null)
                return;

            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }

        public void DrawCircleAndDamageToEnemies()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].TryGetComponent(out PlayerHealth player))
                {
                    if (player.enabled)
                        player.TakeDamage(_attackDamage);
                }
            }
        }
    }
}