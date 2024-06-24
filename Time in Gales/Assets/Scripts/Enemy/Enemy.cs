using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected NavMeshAgent agent;


    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.Player;
        GameManager.Instance.EnemyDamageGivenEvent.AddListener(EnemyDamageGivenEventListener);
    }


    protected virtual void EnemyDamageGivenEventListener(float damage, GameObject id)
    {
        if (id.GetInstanceID() == GetInstanceID())
        {
            health -= damage;

            if (health < 0)
            {
                GameManager.Instance.EnemyDamageGivenEvent.RemoveListener(EnemyDamageGivenEventListener);
                Destroy(this.gameObject);
            }
                
        }
        Destroy(this.gameObject);
    }
}


public class EnemyState
{
    protected FSMSTATE currentState;
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
    
    protected virtual EnemyState Process()
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