using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected const float TotalHeatCapacity = 10;
    protected float currentHeatup = 0;
    [Header("Specs of the Gun")]
    [SerializeField] protected float damagePoints;
    [SerializeField] protected float normalCoolDownSeconds;
    [SerializeField] protected float recoveryFromPrimaryFire;
    [SerializeField] protected float recoveryFromSecondaryFire;
    [SerializeField] protected float heatupPerShot;
    [SerializeField] protected Transform gunTip;
    [SerializeField] protected GameObject projectile;
    protected bool isCool;
    
    public virtual void PrimaryFire()
    {}

    public virtual void SecondaryFire()
    {}

    protected virtual IEnumerator Recover (FIRETYPE fireType)
    {
        yield return null;
    }
}


public enum FIRETYPE
{
    PRIMARY,
    SECONDARY,
    BOTH
}
