using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _detectDistance = 0.5f;
        private Animator _animator;

        private bool _canRun = true;
        public bool canAttack = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            DetectPlayerInArea();

            if (_target == null) return;

            // Player'ýn pozisyonunu alýn
            Vector2 playerPosition = _target.transform.position;

            // Düþmanýn pozisyonunu alýn
            Vector2 enemyPosition = transform.position;

            // Player'a doðru bir vektör hesaplayýn
            Vector2 direction = playerPosition - enemyPosition;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            direction.Normalize(); // Vektörü normalize et
            float distance = Vector2.Distance(playerPosition, enemyPosition);

            if (distance < _attackDistance)
            {
                if (canAttack)
                {
                    StartCoroutine(Attack());
                }
            }

            if (_canRun)
            {
                // Düþmaný Player'a doðru hareket ettir
                transform.Translate(direction * _speed * Time.deltaTime);
                _animator.SetInteger("AnimState", 2);
            }
            else if (!_canRun && !canAttack)
            {
                transform.Translate(-direction * _speed * Time.deltaTime);
                _animator.SetInteger("AnimState", 2);
            }
        }


        private IEnumerator Attack()
        {
            _animator.SetTrigger("Attack");
            canAttack = false;
            yield return new WaitForSeconds(0.5f);
            _canRun = false;
            yield return new WaitForSeconds(1f);
            _canRun = true;
            yield return new WaitForSeconds(0.5f);
            canAttack = true;
        }

        private void DetectPlayerInArea()
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, _detectDistance, _playerLayer);

            if (player != null)
            {
                _target = player.gameObject;
            }
            else
            {
                _target = null;
                _animator.SetInteger("AnimState", 1);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _detectDistance);
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}

