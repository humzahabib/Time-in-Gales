using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TimeFreaksWithGuns : TimeFreak
{
    [SerializeField] float reactionTime;
    [SerializeField] Gun gun;
    //[SerializeField] GameObject effect;
    //[SerializeField] GameObject effectDeath;
    bool visible = false;
    private float timepass;
    

    
public bool Visible
    { get { return visible; } }
public float ReactionTime
    { get { return reactionTime; } }

    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
        state = new TimeFreakWithGunsChaseState(animator, player, this, agent, gun);

        GameManager.Instance.EnemyDamageGivenEvent.AddListener(EnemyDamageGivenEventListener);
        if (timepass >= 5f)
        {
            timepass = 0f;
            if(timeFreakSound != null)
            {
                GameManager.Instance.AudioManager.Play(timeFreakSound);
            }
        }
        timepass += Time.deltaTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (state != null)
        {
            state = state.Process();
        }
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
    
    protected override void EnemyDamageGivenEventListener(float damage, GameObject id)
    {
        base.EnemyDamageGivenEventListener(damage, id);

        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(InstantiateEffect(effect));
    }

    IEnumerator InstantiateEffect(GameObject effect)
    {
        if (effect != null)
        {
            GameObject myeffect = Instantiate(effect, transform.position, Quaternion.identity);
            myeffect.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            myeffect.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if(health < 1f)
        {
            GameManager.Instance.EnemyDeadEvent.Invoke(transform.position, effectDeath);
            GameManager.Instance.EnemyDamageGivenEvent.RemoveListener(EnemyDamageGivenEventListener);
            state = null;
        }
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
            angularSpeed = 0;

            Vector3 lookDir = target.transform.position - self.transform.position;
            lookDir.Normalize();

            float angle = Vector3.SignedAngle(self.transform.forward, lookDir, Vector3.up);

            if (angle < 10f)
            {
                if (elapsedSeconds >= self.ReactionTime)
                {
                    gun.PrimaryFire();
                    elapsedSeconds = 0;
                }
            }
            else
            {
                self.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * 5f);
            }
        }
        else
        {
            agent.angularSpeed = 40;
        }

        if (agent.velocity.magnitude < 0.1f)
        {
            angularSpeed = 0;

            Vector3 lookDir = target.transform.position - self.transform.position;
            lookDir.Normalize();


            float angle = Vector3.SignedAngle(self.transform.forward, lookDir, Vector3.up);
            self.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * 5f);

        }
        agent.stoppingDistance = self.AttackRange;
        agent.SetDestination(target.transform.position);
    }


    protected virtual bool CanShoot()
    {
        Ray ray = new Ray(self.transform.position, target.transform.position - self.transform.position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity) && target != null)
        {
            return (hit.transform.tag == target.transform.tag);
        }
        return false;
    }
}
