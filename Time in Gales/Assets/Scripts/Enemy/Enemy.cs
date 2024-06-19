using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject target;
    [SerializeField] protected NavMeshAgent agent;
    protected EnemyState state;

    [SerializeField] protected float fovRadius;
    [SerializeField, Range(0, 360)] protected int fovAngle;
    [SerializeField] protected float attackRange;


    public float AttackRange
    {
        get {
            return attackRange; 
        }
    }


    public virtual bool CanSeeTarget()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        targetDir.y = 0;


        if (Mathf.Abs(Vector3.Angle(transform.forward, targetDir)) < fovAngle / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDir.normalized, out hit, fovRadius))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    return true;
                }
            }
        }

        return false;
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