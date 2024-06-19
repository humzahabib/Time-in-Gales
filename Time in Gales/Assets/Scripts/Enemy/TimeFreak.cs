using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class TimeFreak : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class TimeFreakIdleState : EnemyState
{
    public TimeFreakIdleState (Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        : base(_anim, _target, _self, _agent)
    {

    }


    protected override void Enter()
    {
        anim.SetBool("IsIdle", true);
        base.Enter();
    }

    protected override void Update()
    {
        if (self.CanSeeTarget())
        {
            nextState = new TimeFreakChaseState(anim, target, self, agent);
        }
    }
}

public class TimeFreakChaseState : EnemyState
{
    public TimeFreakChaseState (Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        :base (_anim, _target, _self, _agent)
    {
    }

    protected virtual void Enter()
    {
        anim.SetBool("isRunning", true);
        currentState = FSMSTATE.UPDATE;
    }

    protected virtual void Update()
    {
        if (agent.destination != target.transform.position)
        {
            agent.SetDestination(target.transform.position);
            anim.SetFloat("WalkingSpeed", (agent.desiredVelocity.magnitude / agent.velocity.magnitude));
            

            Vector3 dirToTarget = target.transform.position - self.transform.position;
            if (dirToTarget.magnitude <= self.AttackRange)
                nextState = new TimeFreakAttackState(anim, target, this, agent);

        }
    }
}


public class TimeFreakAttackState
{
    public TimeFreakAttackState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
}
