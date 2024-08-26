using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeControlAttack
{
    Click, //we are declaring that we can click to shoot or hold for multiple shots
    Hold
}


public enum TypeWeapon
{
    Melee,
    OneHand,
    TwoHand
}

[System.Serializable] //to have it visible in inspector panel
public struct DefaultConfig
{

    public TypeControlAttack typeControl;
    public TypeWeapon typeWeapon;

    [Range(0,100)]
    public int damage;

    [Range(0, 100)]
    public int criticalDamage;

    [Range(0.01f , 1.0f)]
    public float fireRate; //determine how much we can fire in a second. For some weapons we will be able to shoot 1 bullet per second. For other weapons, we may shoot a bullet in 0.1 seconds.

    [Range(0, 100)]
    public int criticalRate;

}

//struct is very similar to a class -nearly %99 similar-. It can have functions,etc. But struct will be passed as a value and function or class will be passed as reference





















