using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TimeFreaksWithGuns : TimeFreak
{
    [SerializeField] float reactionTime;
    [SerializeField] Gun gun;
    bool visible = false;
    

    
public bool Visible
    { get { return visible; } }
public float ReactionTime
    { get { return reactionTime; } }

    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        state = new TimeFreakWithGunsChaseState(animator, player, this, agent, gun);

        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        state = state.Process();
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
}






public class TimeFreakWithGunsChaseState: EnemyState
{
    Gun gun;
    float angularSpeed;
    new TimeFreaksWithGuns self;
    public TimeFreakWithGunsChaseState(Animator _anim, GameObject _target, Enemy _self, NavMeshAgent _agent, Gun _gun) 
        : base(_anim, _target, _self, _agent)
    {
        gun = _gun;
        self = (TimeFreaksWithGuns)_self;
        angularSpeed = _agent.angularSpeed;
    }


    protected override void Enter()
    {
        anim.SetBool("isRunning", true);
        base.Enter();
    }
    float elapsedSeconds;
    protected override void Update()
    {
        anim.SetFloat("Movement", agent.velocity.magnitude / agent.speed);
        elapsedSeconds += Time.deltaTime;

        if (CanShoot())
        {
            Debug.Log("CanShoot");
            angularSpeed = 0;

            Vector3 lookDir = target.transform.position - self.transform.position;
            lookDir.Normalize();

            float angle = Vector3.SignedAngle(self.transform.forward, lookDir, Vector3.up);

            if (angle < 2f)
            {
                if (elapsedSeconds >= self.ReactionTime)
                {
                    gun.PrimaryFire();
                    elapsedSeconds = 0;
                }
            }
            else
            {
                self.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * 10f);
            }
        }
        else
        {
            agent.angularSpeed = angularSpeed;
        }

        if (agent.velocity.magnitude < 0.2f)
        {
            angularSpeed = 0;

            Vector3 lookDir = target.transform.position - self.transform.position;
            lookDir.Normalize();


            float angle = Vector3.SignedAngle(self.transform.forward, lookDir, Vector3.up);
            self.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * 10f);

        }
        agent.stoppingDistance = self.AttackRange;
        agent.SetDestination(target.transform.position);
    }


    protected virtual bool CanShoot()
    {
        Ray ray = new Ray(self.transform.position, target.transform.position - self.transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return (hit.transform.tag == target.transform.tag);
    }
}
