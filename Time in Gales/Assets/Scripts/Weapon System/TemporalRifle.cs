using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalRifle : Gun
{
    
    bool isPrimaryCool, isSecondaryCool, canPrimary, canSecondary;
    [SerializeField] bool isPlayers;
    [SerializeField] protected float secondaryPoints;


public bool IsPlayers
        { get { return isPlayers; } set { isPlayers = value; } }

    // Start is called before the first frame update
    void Start()
    {
        isPrimaryCool = true;
        isSecondaryCool = true;
        canPrimary = true;
        canSecondary = true;
        currentHeatup = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayers)
        {
            GameManager.Instance.HeatupValueChange.Invoke(currentHeatup);
            if (currentHeatup >= TotalHeatCapacity)
            {
                isPrimaryCool = false;
                isSecondaryCool = false;
                StartCoroutine(Recover(FIRETYPE.BOTH));
            }



            currentHeatup = Mathf.Clamp(currentHeatup, 0, TotalHeatCapacity);
            if (Input.GetMouseButton(0))
                PrimaryFire();
            else
                currentHeatup -= normalCoolDownSeconds * Time.deltaTime;
            if (Input.GetMouseButton(1))
                SecondaryFire();
        }
    }

    public override void PrimaryFire()
    {
        if (isPrimaryCool && canPrimary)
        {
                GameObject.Instantiate(projectile, gunTip.position, gunTip.rotation);
                currentHeatup += heatupPerShot;
                StartCoroutine(Recover(FIRETYPE.PRIMARY));
        }

        if (isPlayers == false)
        {
            isPrimaryCool = true;
            isSecondaryCool = true;
            canPrimary = true;
            canSecondary = true;
        }
    }

    public override void SecondaryFire()
    {
        if (canSecondary && isSecondaryCool)
        {
            if (Input.GetMouseButton(1))
            {
                currentHeatup -= secondaryPoints;
            }
            GameManager.Instance.HeatupValueChange.Invoke(currentHeatup);
        }
    }

    protected override IEnumerator Recover(FIRETYPE fireType)
    {
        if (fireType == FIRETYPE.PRIMARY)
        {
            canPrimary = false;
            yield return new WaitForSeconds(recoveryFromPrimaryFire);

            canPrimary = true;
        }
        else if (fireType == FIRETYPE.SECONDARY)
        {
            canSecondary = false;
            yield return new WaitForSeconds(recoveryFromSecondaryFire);
            canSecondary = true;
        }
        else if (fireType == FIRETYPE.BOTH)
        {
            isPrimaryCool = false;
            isSecondaryCool = false;
            yield return new WaitForSeconds(normalCoolDownSeconds);
            currentHeatup = 0;
            isSecondaryCool = true;
            isPrimaryCool = true;

            yield return null;
        }

        yield return null;
}
}
