using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CronoPistol : Gun
{
    [SerializeField] protected GameObject gernadeEffect;
    [SerializeField] protected float rangeForGernade;
    
    


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrimaryFire();
        }
    }
    public override void PrimaryFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Firing");
            // Fire
            GameObject.Instantiate(projectile, gunTip.transform.position, gunTip.rotation);

            
        }
    }

    public override void SecondaryFire()
    {
        // Fire

        if (isCool)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //Fire
                StartCoroutine(Recover(FIRETYPE.SECONDARY));
            }
        }
    }


    protected override IEnumerator Recover(FIRETYPE fireType)
    {
        if (fireType == FIRETYPE.SECONDARY)
        {
            isCool = false;
        }
        yield return new WaitForSeconds(recoveryFromSecondaryFire);
        isCool = true;
    }


}
