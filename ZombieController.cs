using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    private ZombieMovement zombie_Movement;
    private ZombieAnimation zombie_Animation;

    private Transform targetTransform;
    private bool canAttack;
    private bool zombie_Alive;

    public GameObject damage_Collider;

    public int zombieHealth = 10;
    public GameObject[] fxDead;

    private float timerAttack;

    private int fireDamage = 10;

    public GameObject coinCollectable;

    // Start is called before the first frame update
    void Start()
    {

        zombie_Movement = GetComponent<ZombieMovement>();
        zombie_Animation = GetComponent<ZombieAnimation>();

        zombie_Alive = true;

       if(GameplayController.instance.zombieGoal == ZombieGoal.PLAYER)
        {

            targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

        } else if (GameplayController.instance.zombieGoal == ZombieGoal.FENCE)
        {

            GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);
            targetTransform = fences[Random.Range(0, fences.Length)].transform;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (zombie_Alive)
        {
            CheckDistance();
        }
    }

    void CheckDistance()
    {
        if (targetTransform) //we have the target distance: means player is alive
        {
            //if the distance between target and the zombie is greater than 1,5 then the zombie will move
            if(Vector3.Distance(targetTransform.position,transform.position) > 1.5f)
            {
                zombie_Movement.Move(targetTransform);
            }
            else
            {
                if (canAttack)
                {
                    zombie_Animation.Attack();

                    timerAttack += Time.deltaTime;

                    if(timerAttack > 0.55f)
                    {
                        timerAttack = 0f;
                        AudioManager.instance.ZombieAttackSound();
                    }
                    
                }
            }
        }
    }

    public void ActivateDeadEffect(int index)
    {
        fxDead[index].SetActive(true);

        if (fxDead[index].GetComponent<ParticleSystem>())
        {
            fxDead[index].GetComponent<ParticleSystem>().Play();
        }
    }

    IEnumerator DeactivateZombie()
    {
        AudioManager.instance.ZombieDieSound();

        yield return new WaitForSeconds(2f);

        GameplayController.instance.ZombieDied();

        Instantiate(coinCollectable,transform.position,Quaternion.identity);

        gameObject.SetActive(false);

    }

    public void DealDamage(int damage)
    {
        zombie_Animation.Hurt();

        zombieHealth -= damage;

        if(zombieHealth <= 0)
        {
            zombie_Alive = false;
            zombie_Animation.Dead();

            StartCoroutine(DeactivateZombie());
        }
    }

    void ActivateDamagePoint()
    {
        damage_Collider.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damage_Collider.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = true;
        }

        if(target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            zombie_Animation.Hurt();

            zombieHealth -= target.gameObject.GetComponent<BulletController>().damage;

            if(target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                target.gameObject.GetComponent<BulletController>().ExplosionFX();
            }

            if(zombieHealth <= 0)
            {
                zombie_Alive = false;
                zombie_Animation.Dead();

                StartCoroutine(DeactivateZombie());
            }


            target.gameObject.SetActive(false); //deactivate bullet/missile
        }

        if (target.tag == TagManager.FIRE_BULLET_TAG)
        {
            zombie_Animation.Hurt();

            zombieHealth -= fireDamage;

            if(zombieHealth <= 0)
            {
                zombie_Alive = false;
                zombie_Animation.Dead();

                StartCoroutine(DeactivateZombie());
            }
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = false;
        }
    }

}//class





















