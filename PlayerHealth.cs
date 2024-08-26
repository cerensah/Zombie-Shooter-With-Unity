using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public GameObject[] blood_FX;

    private PlayerAnimations playerAnim;

    /*
    //think of event as a a subscription to a newspaper.It checks if the subscription is still there(not null) -DealDamage()'ın içinde kontrol ettiğimiz gibi-. Eğer null değilse event gerçekleşecek
    public delegate void PlayerDeadEvent(bool dead);
    public static event PlayerDeadEvent playerDead;
    */

    void Awake()
    {
        playerAnim = GetComponentInParent<PlayerAnimations>();
    }


    public void DealDamage(int damage)
    {

        health -= damage;

        GameplayController.instance.PlayerLifeCounter(health);

        playerAnim.HurtAnimation();

        if(health <= 0)
        {

            GameplayController.instance.playerAlive = false;

            GetComponent<Collider2D>().enabled = false;
            playerAnim.DeadAnimation();

            blood_FX[Random.Range(0, blood_FX.Length)].SetActive(true);

            GameplayController.instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "HealthKit" && health < 100)
        {
            health += 10;
            GameplayController.instance.PlayerLifeCounter(health);
        }
    }

}//class


























