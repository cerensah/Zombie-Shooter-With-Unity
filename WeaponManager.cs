using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public List<WeaponController> weapons_Unlocked;
    public WeaponController[] total_Weapons;

    [HideInInspector]
    public WeaponController current_Weapon;
    private int current_Weapon_Index;

    private TypeControlAttack current_TypeControl;

    private PlayerArmController[] armController;

    private PlayerAnimations playerAnim;

    private bool isShooting;

    public GameObject meleeDamagePoint;

    void Awake()
    {
        playerAnim = GetComponent<PlayerAnimations>();

        loadActiveWeapons();

        current_Weapon_Index = 1;
    }

    void Start()
    {
        armController = GetComponentsInChildren<PlayerArmController>();

        //sset the first weapon to be pistol
        ChangeWeapon(weapons_Unlocked[1]);

        playerAnim.SwitchWeaponAnimation(
            (int)weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon); //if melee equal to 0, onearm equal to 1... Animator'da da type_weapon için melee 0, one hand 1, two hand 2 demiştik

    }

    void loadActiveWeapons()
    {
        weapons_Unlocked.Add(total_Weapons[0]);

        for(int i = 1; i < total_Weapons.Length; i++) {

            weapons_Unlocked.Add(total_Weapons[i]);
        }
    }

    public void SwitchWeapon()
    {
        current_Weapon_Index++;

        current_Weapon_Index =
            (current_Weapon_Index >= weapons_Unlocked.Count) ? 0 : current_Weapon_Index; //en sondaki silaha gelince başa geri dönmek için
       
        /*  yukarıdaki kodun açıklaması.  Yukarıdai : işareti else anlamına geliyor
        if (current_Weapon_Index >= weapons_Unlocked.Count)
        {
            current_Weapon_Index = 0;
        }
        else
        {
            current_Weapon_Index = current_Weapon_Index;
        } */

        playerAnim.SwitchWeaponAnimation(
            (int)weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon); 

        ChangeWeapon(weapons_Unlocked[current_Weapon_Index]);

    }

    void ChangeWeapon(WeaponController newWeapon)
    {
        if (current_Weapon)
            current_Weapon.gameObject.SetActive(false);

        current_Weapon = newWeapon;
        current_TypeControl = newWeapon.defaultConfig.typeControl;

        newWeapon.gameObject.SetActive(true);

        if(newWeapon.defaultConfig.typeWeapon == TypeWeapon.TwoHand)
        {
            for(int i = 0; i< armController.Length; i++)
            {
                armController[i].ChangeToTwoHand();
            }
        }
        else
        {
            for (int i = 0; i < armController.Length; i++)
            {
                armController[i].ChangeToOneHand();
            }
        }       
    }

    public void Attack()
    {
        if(current_TypeControl == TypeControlAttack.Hold)
        {
            current_Weapon.CallAttack();

        } else if(current_TypeControl == TypeControlAttack.Click) {

            if (!isShooting)
            {
                current_Weapon.CallAttack();
                isShooting = true;
            }
        }
    }

    public void ResetAttack()
    {
        isShooting = false;
    }

    void AllowCollisionDetection()
    {

        meleeDamagePoint.SetActive(true);
    }

    void DenyCollisionDetection()
    {

        meleeDamagePoint.SetActive(false);
    }

} //class



























