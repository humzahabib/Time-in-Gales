using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBeastAnimationEventHandler : EnemyAnimationEventHandler
{


    protected override void Start()
    {
    }


    public override void AttackEventCall()
    {
        Collider[] colliders = Physics.OverlapSphere(hand.position, attackRadius);

        foreach (Collider coll in colliders)
        {
            if (coll.tag == "Player")
            {
                GameManager.Instance.PlayerHealthChangeEvent.Invoke(attackDamage);
            }
        }
    }


    
}
