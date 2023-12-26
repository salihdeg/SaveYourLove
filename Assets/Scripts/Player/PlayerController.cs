using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;

        [HideInInspector] public float _xInput;
        [HideInInspector] public float playerTurnScale = 1f;

        private Rigidbody2D _rb;
        private Animator _animator;

        public static bool isBlocking = false;
        public static bool isStop = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            //StartCoroutine(GetXInput());
        }

        private void Update()
        {
            if (isStop)
            {
                _xInput = 0;
                RunAnimation(_xInput);
                return;
            }
            if (PlayerDash.isDashing) return;

            _xInput = Input.GetAxis("Horizontal");

            BlockingAnimation();

            TurnPlayer(_xInput);
            RunAnimation(_xInput);
        }

        private void FixedUpdate()
        {
            if (isStop)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                return;
            }
            if (PlayerDash.isDashing) return;

            if (!isBlocking && !PlayerAttack.isAttacking)
                Move(_xInput);
        }

        private void Move(float moveInput)
        {
            if (_rb != null)
                _rb.velocity = new Vector2(moveInput * _moveSpeed, _rb.velocity.y);
        }

        private IEnumerator GetXInput()
        {
            if (isStop)
            {
                RunAnimation(_xInput);
                yield return null;
                StartCoroutine(GetXInput());
            }
            else
            {
                _xInput = Input.GetAxis("Horizontal");

                TurnPlayer(_xInput);
                RunAnimation(_xInput);

                yield return null;
                StartCoroutine(GetXInput());
            }
        }

        private void RunAnimation(float input)
        {
            if (input != 0)
            {
                _animator.SetBool(AnimationStates.isRunning, true);
            }
            else
            {
                _animator.SetBool(AnimationStates.isRunning, false);
            }
        }

        private void TurnPlayer(float moveInput)
        {
            if (moveInput > 0)
                playerTurnScale = 1f;
            else if (moveInput < 0)
                playerTurnScale = -1f;

            transform.localScale = new Vector3(playerTurnScale, 1f, 1f);
        }

        private void BlockingAnimation()
        {
            if (Input.GetMouseButtonDown(1))
                isBlocking = true;
            else if (Input.GetMouseButtonUp(1))
                isBlocking = false;
            _animator.SetBool(AnimationStates.isBlocking, isBlocking);
        }
    }
}

