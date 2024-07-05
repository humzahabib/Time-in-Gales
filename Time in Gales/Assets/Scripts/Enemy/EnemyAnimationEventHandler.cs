using UnityEngine;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    [SerializeField] protected Transform hand;
    protected float attackRadius;
    protected float attackDamage;
    protected LayerMask playerLayer;
    [SerializeField] protected Enemy parent;
    protected virtual void Start()
    {
        parent = transform.parent.GetComponent<Enemy>();
        attackRadius = parent.AttackRadius;
        playerLayer = parent.PlayerLayer;
        attackDamage = parent.AttackDamage;
        hand = parent.Hand;
    }


    public virtual void AttackEventCall()
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
