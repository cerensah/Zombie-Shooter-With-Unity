using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioClip[] gunSounds;
    public AudioClip meleeSound;

    public AudioSource playerAttack_AudioSource;

    public AudioSource zombieAttack_AudioSource;
    public AudioSource zombieRise_AudioSource;
    public AudioSource zombieDie_AudioSource;

    public AudioClip zombieRise_Clip, zombieDie_Clip;
    public AudioClip[] zombieAttack_Clip;

    public AudioSource fenceExplosion_AudioSource;
    public AudioClip fenceExplosion_Clip;

    public AudioSource coin_AudioSource;
    public AudioClip coin_Clip;

    public AudioSource kit_AudioSource;
    public AudioClip kit_Clip;

    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        //if we have a duplicate instance then destroy
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GunSound(int index)
    {
        playerAttack_AudioSource.PlayOneShot(gunSounds[index], 1.0f);
    }

    public void MeleeAttackSound()
    {
        playerAttack_AudioSource.PlayOneShot(meleeSound, 1.0f);
    }

    public void ZombieRiseSound()
    {
        zombieRise_AudioSource.PlayOneShot(zombieRise_Clip, 1.0f);
    }

    public void ZombieDieSound()
    {
        zombieDie_AudioSource.PlayOneShot(zombieDie_Clip, 1.0f);
    }

    public void ZombieAttackSound()
    {
        int index = Random.Range(0, zombieAttack_Clip.Length);
        zombieAttack_AudioSource.PlayOneShot(zombieAttack_Clip[index], 1.0f);
    }

    public void FenceExplosion()
    {
        fenceExplosion_AudioSource.PlayOneShot(fenceExplosion_Clip, 1.0f);
    }

    public void CoinSound()
    {
        coin_AudioSource.PlayOneShot(coin_Clip, 1.0f);
    }

    public void HealthKitSound()
    {
        kit_AudioSource.PlayOneShot(kit_Clip, 1.0f);
    }

} //class


























