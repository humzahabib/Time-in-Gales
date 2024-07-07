using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class TimeFreak : Enemy
{

    [SerializeField] protected AudioClip timeFreakSound;
    private float timepass;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        GameManager.Instance.EnemyDamageGivenEvent.AddListener(EnemyDamageGivenEventListener);
        state = new TimeFreakChaseState(animator, player, this, agent);
        timepass = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        state = state.Process();
        if (timepass >= 10f)
        {
            timepass = 0f;
            if(timeFreakSound != null)
            {
                GameManager.Instance.AudioManager.Play(timeFreakSound);
            }
        }
        timepass += Time.deltaTime;
    }

    protected override void EnemyDamageGivenEventListener(float damage, GameObject id)
    {

        Debug.Log("Thain thain ho gyi");
        base.EnemyDamageGivenEventListener(damage, id);
    }
}







public class TimeFreakChaseState : EnemyState
{
    public TimeFreakChaseState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        : base(_anim, _target, _self, _agent)
    {
    }

    protected override void Enter()
    {
        anim.SetBool("isRunning", true);
        currentState = FSMSTATE.UPDATE;
    }

    protected override void Update()
    {
        if (agent.destination != target.transform.position)
        {
            agent.SetDestination(target.transform.position);
            anim.SetFloat("Movement", (agent.velocity.magnitude / agent.speed));


            Vector3 dirToTarget = target.transform.position - self.transform.position;
            if (dirToTarget.magnitude <= self.AttackRange)
            {
                nextState = new TimeFreakAttackState(anim, target, self, agent);
                currentState = FSMSTATE.EXIT;
            }
        }
    }

    protected override void Exit()
    {
        anim.SetBool("isRunning", false);
    }
}


public class TimeFreakAttackState : EnemyState
{
    public TimeFreakAttackState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        : base(_anim, _target, _self, _agent)
    {

    }

    protected override void Enter()
    {
        anim.SetBool("isAttacking", true);
        currentState = FSMSTATE.UPDATE;
    }

    protected override void Update()
    {
        if (Vector3.Distance(self.transform.position, target.transform.position) < self.AttackRange)
        {
            nextState = new TimeFreakChaseState(anim, target, self, agent);
        }
    }
}


public class TimeFreakFurryState : EnemyState
{
    public TimeFreakFurryState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
    : base(_anim, _target, _self, _agent)
    { }

}

