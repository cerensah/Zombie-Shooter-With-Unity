using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public int health = 10;
    public ParticleSystem wood_Break_FX, wood_Explode_FX;

    public GameObject kitCollectable;

    /*
    public void DealDamage(int damage)
    {
        health -= damage;
        wood_Break_FX.Play();

        if (health <= 0)
        {
            wood_Explode_FX.Play();
            AudioManager.instance.FenceExplosion();

            GameplayController.instance.fenceDestroyed = true;

            StartCoroutine(DeactivateGameObject());

        }
    }*/

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG)
        {

            health -= target.gameObject.GetComponent<BulletController>().damage;

            if (target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                target.gameObject.GetComponent<BulletController>().ExplosionFX();
            }

            target.gameObject.SetActive(false); //deactivate bullet/missile

            wood_Break_FX.Play();

            if (health <= 0)
            {
                wood_Explode_FX.Play();
                AudioManager.instance.FenceExplosion();

                GameplayController.instance.fenceDestroyed = true;

                StartCoroutine(DeactivateGameObject());

            }
        }
    }

    IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);

        Instantiate(kitCollectable, transform.position, Quaternion.identity);

    }
}
