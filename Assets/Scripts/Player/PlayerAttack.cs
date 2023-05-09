using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;

        [HideInInspector] public bool isAttacking = false;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            StartCoroutine(CheckAttackInput());
        }

        private IEnumerator CheckAttackInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                _animator.SetTrigger(AnimationStates.isAttacking);
            }


            yield return null;
            StartCoroutine(CheckAttackInput());
        }

        private void Attack()
        {
            // Play an attack anim
            // Detect enemies in range
            // Damage them
        }
    }
}