using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] protected float health;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform hand;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected LayerMask playerLayer;

    protected EnemyState state;


public Transform Hand
    { get { return hand; } }
public float AttackRadius
    { get { return attackRadius; } } 
public float AttackDamage
    { get { return attackDamage; } }
 public float AttackRange
    { get { return attackRange; } }
public LayerMask PlayerLayer
    { get { return playerLayer; } }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hand.transform.position, attackRadius);
    }
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
        player = GameManager.Instance.Player;
    }

    private void Update()
    {
    }

    protected virtual void AttackEventCall()
    {
        Collider[] colliders = Physics.OverlapSphere(hand.position, attackRadius, playerLayer);

        foreach (Collider coll in colliders)
        {
            if (coll.tag == "Player")
            {
                GameManager.Instance.PlayerHealthChangeEvent.Invoke(attackDamage);
            }
        }
    }

    protected virtual void EnemyDamageGivenEventListener(float damage, GameObject id)
    {
        
        if (id.GetInstanceID() == this.gameObject.GetInstanceID())
        {

            Debug.Log("Thain thain");
            health -= damage;

            if (health <= 0)
            {
                GameManager.Instance.EnemyDamageGivenEvent.RemoveListener(EnemyDamageGivenEventListener);
                GameManager.Instance.EnemyDeadEvent.Invoke();
                Destroy(this.gameObject);
            }
                
        }
    }
}


public class EnemyState
{
    protected FSMSTATE currentState = FSMSTATE.ENTER;
    protected Animator anim;
    protected Enemy self;
    protected GameObject target;
    protected NavMeshAgent agent;
    protected EnemyState nextState;
    public EnemyState (Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
    {
        anim = _anim;
        target = _target;
        self = _self;
        agent = _agent;
        currentState = FSMSTATE.ENTER;
        nextState = this;
    }

    protected virtual void Enter() 
    {
        currentState = FSMSTATE.UPDATE;
    }
    protected virtual void Update() { }
    protected virtual void Exit() { }
    
    public virtual EnemyState Process()
    {
        if (currentState == FSMSTATE.ENTER)
            Enter();
        else if (currentState == FSMSTATE.UPDATE)
            Update();
        else if (currentState == FSMSTATE.EXIT)
            Exit();

        return nextState;
    }
    
   

}