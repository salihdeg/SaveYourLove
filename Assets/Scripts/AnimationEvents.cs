using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;

    public void SetIsAttackingFalse()
    {
        PlayerAttack.isAttacking = false;
    }

    public void AttackEnemies()
    {
        _playerAttack.DrawCircleAndDamageToEnemies();
    }
}
