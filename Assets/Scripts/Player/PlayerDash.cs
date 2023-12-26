using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDash : MonoBehaviour
    {
        public static bool isDashing;
        private bool _canDash = true;
        [SerializeField] private float _dashPower = 12f;
        [SerializeField] private float _dashCooldown = 1.5f;
        private float _dashTime = 0.2f;

        private TrailRenderer _trailRenderer;
        private Rigidbody2D _rb;
        private Animator _animator;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (isDashing) return;
            if (PlayerController.isStop) return;
            if (PlayerController.isBlocking) return;
            if (PlayerAttack.isAttacking) return;

            if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
            {
                StartCoroutine(Dash());
            }
        }

        private IEnumerator Dash()
        {
            _canDash = false;
            isDashing = true;
            _animator.SetBool("Dashing", isDashing);
            float originalGravity = _rb.gravityScale;
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(transform.localScale.x * _dashPower, 0f);
            _trailRenderer.emitting = true;
            yield return new WaitForSeconds(_dashTime);
            _trailRenderer.emitting = false;
            _rb.gravityScale = originalGravity;
            isDashing = false;
            _animator.SetBool("Dashing", isDashing);
            yield return new WaitForSeconds(_dashCooldown);
            _canDash = true;
        }
    }
}