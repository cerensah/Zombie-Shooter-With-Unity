using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NameWeapon //to represent the name of our weapons
{
    MELEE,
    PISTOL,
    MP5,
    M3,
    AK,
    AWP,
    FIRE,
    ROCKET
}

public class WeaponController : MonoBehaviour
{
    public DefaultConfig defaultConfig;
    public NameWeapon nameWp;

    protected PlayerAnimations playerAnim;
    protected float lastShot;

    public int gunIndex;
    public int currentBullet;
    public int bulletMax;


    void Awake()
    {
        playerAnim = GetComponentInParent<PlayerAnimations>(); //bu kodu silaha koyacağız o yüzden parent'ı player olacak
        currentBullet = bulletMax;
    }

    public void CallAttack()
    {
        if(Time.time > lastShot + defaultConfig.fireRate) //time.time= passed since the game started
        { //if its true, it means we have already shot and can shoot again

            if(currentBullet > 0) //we have bullet
            {
                ProcessAttack();

                //animate SHoot
                playerAnim.AttackAnimation();

                lastShot = Time.time;
                currentBullet--;
            }
            else //no amm0
            {
                //play no ammo sound
            }
        } 
    }

    public virtual void ProcessAttack() //virtual since we want to override/modify it for each weapon
    {

    }


} //class




























