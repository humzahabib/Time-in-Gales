using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    protected Transform hand;
    protected float attackRadius;
    protected float attackDamage;
    protected LayerMask playerLayer;

    protected virtual void Start()
    {
        Enemy parent = transform.parent.GetComponent<Enemy>();
        hand = parent.Hand;
        attackRadius = parent.AttackRadius;
        playerLayer = parent.PlayerLayer;
        attackDamage = parent.AttackDamage;
    }


    public void AttackEventCall()
    {
        Collider[] colliders = Physics.OverlapSphere(hand.position, attackRadius);

        foreach (Collider coll in colliders)
        {
            if (coll.tag == "Player")
            {
                GameManager.Instance.PlayerDamageEvent.Invoke(attackDamage);
            }
        }
    }
}
