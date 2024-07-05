using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TimeBeast : Enemy
{

    [SerializeField] protected float normalRotationSpeed;
    [SerializeField] protected float chargeRotationSpeed;
    [SerializeField] protected float chargeSpeed;
    [SerializeField] protected float chargeDuration;

    public UnityEvent<bool> HeadButtEvent = new UnityEvent<bool>();

public float RotationSpeed
    { get { return normalRotationSpeed; } }

public float ChargeRotationSpeed
    { get { return chargeRotationSpeed; } }

public float ChargeSpeed
    { get { return chargeSpeed; } }

public float ChargeDuration
    { get { return chargeDuration; } }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        state = new TimeBeastIdleState(animator, player, this, agent);
    }

    // Update is called once per frame
    void Update()
    {
        state = state.Process();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && state.GetType() == typeof(TimeBeastChargeState))
        {
            HeadButtEvent.Invoke(true);
        }
    
    }
}


public class TimeBeastIdleState : EnemyState
{
    public TimeBeastIdleState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        : base(_anim, _target, _self, _agent)
    {
        anim = _anim;
        target = _target;
        self = _self;
        agent = _agent;
    }


    protected override void Enter()
    {
        anim.SetFloat("Walk", 0);
        Debug.Log("Entered Idle Enter");
        base.Enter();
    }

    protected override void UpdateState()
    {
        base.UpdateState();
        currentState = FSMSTATE.EXIT;
    }
    protected override void Exit()
    {
        nextState = new TimeBeastChaseState(anim, target, self, agent);
    }
}


public class TimeBeastChaseState : EnemyState
{
    public TimeBeastChaseState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        : base(_anim, _target, _self, _agent)
    {
        anim = _anim;
        target = _target;
        self = _self;
        agent = _agent;
    }


    protected override void Enter()
    {
        agent.speed = self.Speed;
        agent.angularSpeed = ((TimeBeast)self).RotationSpeed;
        Debug.Log("Entered Chase Enter");
        base.Enter();
    }

    protected override void UpdateState()
    {
        Vector3 dist = target.transform.position - self.transform.position;
        anim.SetFloat("Walk", agent.velocity.magnitude / agent.speed);
        if (dist.magnitude < self.AttackRange)
        {
            anim.SetTrigger("Melee");
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
        Debug.Log("Chasing");
        self.StartCoroutine(SwitchToCharge());
    }


    IEnumerator SwitchToCharge()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);

            if (Random.value > 0.10f)
            {
                nextState = new TimeBeastChargeState(anim, target, self, agent);
            }
        }
    }
}



public class TimeBeastChargeState : EnemyState
{
    bool raging = false;
    float duration;
    bool canContinue = true;
    public TimeBeastChargeState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
    : base(_anim, _target, _self, _agent)
    {
        anim = _anim;
        target = _target;
        self = _self;
        agent = _agent;
    }

    protected override void Enter()
    {
        agent.speed = ((TimeBeast)self).ChargeSpeed;
        agent.angularSpeed = ((TimeBeast)self).ChargeRotationSpeed;
        self.StartCoroutine(Rage(1.25f));
        agent.speed = 0;
        base.Enter();
    }

    protected override void UpdateState()
    {
        while (canContinue)
        {
            Vector3 dir = target.transform.position - self.transform.position;
            dir.Normalize();
            if (!raging)
            {
                Vector3 position = self.transform.position;
                position += dir * ((TimeBeast)self).ChargeSpeed * Time.deltaTime;
                self.transform.position = position;

            }
            else
            {
                float angle = Vector3.SignedAngle(self.transform.forward, dir, Vector3.up);
                self.transform.Rotate(Vector3.up, angle * Time.deltaTime * ((TimeBeast)self).ChargeRotationSpeed);

                duration = dir.magnitude / ((TimeBeast)self).ChargeSpeed;
            }
        }
    }

    protected override void Exit()
    {
        agent.speed = self.Speed;
        agent.angularSpeed = ((TimeBeast)self).RotationSpeed;
        nextState = new TimeBeastChaseState(anim, target, self, agent);
    }

    
    IEnumerator Rage(float seconds)
    {
        raging = true;

        agent.isStopped = true;
        anim.SetTrigger("Rage");

        yield return new WaitForSeconds(1.25f);
        self.StartCoroutine(ContinueCharge(duration));
        agent.isStopped = false;
        raging = false;
    }

    IEnumerator ContinueCharge(float time)
    {
        yield return new WaitForSeconds(time);
        nextState = new TimeBeastChargeState(anim, target, self, agent);
    }

    IEnumerator HeadButtHandler(bool player)
    {
        anim.SetTrigger("HeadButt");
        yield return new WaitForSeconds(1.25f);
        if (false)
            nextState = new TimeBeastStunnedState(anim, target, self, agent);
        else
        {
            yield return new WaitForSeconds(0.1f);
            nextState = new TimeBeastChargeState(anim, target, self, agent);
        }

    }

}

public class TimeBeastStunnedState : EnemyState
{
    public TimeBeastStunnedState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent)
        : base(_anim, _target, _self, _agent)
    {
        anim = _anim;
        target = _target;
        self = _self;
        agent = _agent;
    }

    protected override void Enter()
    {
        agent.speed = 0;
        agent.angularSpeed = 0;
        base.Enter();
    }

    protected override void UpdateState()
    {
        anim.SetFloat("Walk", 0);   
        self.StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(2);
        nextState = new TimeBeastChaseState(anim, target, self, agent);
    }
}
