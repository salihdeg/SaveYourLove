using UnityEngine;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _foot;
        [SerializeField] private LayerMask _touchableLayers;

        [Header("Attribures")]
        [SerializeField] private float _jumpForce = 7.0f;
        [SerializeField] private float _jumpTime = 0.3f;
        [SerializeField] public bool isJumping = false;
        [SerializeField] public static bool isGrounded = true;

        private float _jumpTimeCounter;
        private float _coyoteTime = 0.2f;
        private float _coyoteTimeCounter;
        private float _jumpBufferTime = 0.2f;
        private float _jumpBufferCounter;

        [Header("")]
        [SerializeField] private float _rayDistance = 0.12f;
        private float _xInput;

        //Anim Constants
        private readonly string JUMP_BOOL_ANIM = "isJumping";
        private readonly string GROUNDED_BOOL_ANIM = "isGrounded";

        private void Update()
        {
            GroundCheck();

            _xInput = Input.GetAxis("Horizontal");
            JumpController();
        }

        #region Jump
        private void JumpController()
        {
            if (PlayerController.isStop) return;

            if (PlayerAttack.isAttacking) return;

            if (PlayerDash.isDashing) return;

            Coyote();
            JumpBuffer();

            if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
            {
                _jumpTimeCounter = _jumpTime;
                Jump();
                isJumping = true;
            }

            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (_jumpTimeCounter > 0f)
                {
                    Jump();
                    _coyoteTimeCounter = 0f;
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
                _coyoteTimeCounter = 0f;
            }

            JumpAnimation();
        }

        private void JumpBuffer()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _jumpBufferCounter = _jumpBufferTime;
            else
                _jumpBufferCounter -= Time.deltaTime;
        }

        private void Coyote()
        {
            if (isGrounded)
                _coyoteTimeCounter = _coyoteTime;
            else
                _coyoteTimeCounter -= Time.deltaTime;
        }

        private void GroundCheck()
        {
            RaycastHit2D hit = Physics2D.Raycast(_foot.transform.position, Vector2.down, _rayDistance, _touchableLayers);
            Debug.DrawRay(_foot.transform.position, Vector3.up * -_rayDistance, Color.red);

            if (hit.collider != null)
                isGrounded = true;
            else
                isGrounded = false;

            AnimationIsGroundBool();
        }

        public void Jump()
        {
            if (_rb != null)
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
        #endregion

        #region Animations

        private void JumpAnimation()
        {
            _animator.SetBool(JUMP_BOOL_ANIM, isJumping);
        }

        private void AnimationIsGroundBool()
        {
            _animator.SetBool(GROUNDED_BOOL_ANIM, isGrounded);
        }
        #endregion
    }
}

