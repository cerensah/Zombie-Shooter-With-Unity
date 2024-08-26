using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HealthKitScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.PLAYER_HEALTH_TAG)
        {
            if(target.GetComponent<PlayerHealth>().health < 100)
            {
                AudioManager.instance.HealthKitSound();
                gameObject.SetActive(false);
            }
        }
    }

}

/*
 * 

// belki can eklemek için değiştiriliğ kullanılabilir
void AttackPlayer()
{
    if (GameplayController.instance.playerAlive)
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

        if (target.tag == TagManager.PLAYER_HEALTH_TAG)
        {
            target.GetComponent<PlayerHealth>().DealDamage(damage);
        }
    }

} */ 