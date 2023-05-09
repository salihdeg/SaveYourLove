using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void SetIsAttackingFalse()
    {
        GetComponentInParent<PlayerAttack>().isAttacking = false;
    }
}
