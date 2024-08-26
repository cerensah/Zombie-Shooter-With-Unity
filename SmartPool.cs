using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour
{
    public static SmartPool instance;

    private List<GameObject> bullet_Fall_Fx = new List<GameObject>();
    private List<GameObject> bullet_Prefabs = new List<GameObject>();
    private List<GameObject> rocket_bullet_Prefabs = new List<GameObject>();

    public GameObject[] zombies;
    private float y_Spawn_Pos_Min = -3.7f, y_Spawn_Pos_Max = -0.36f;
    private Camera mainCamera;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        mainCamera = Camera.main;

        InvokeRepeating("StartSpawningZombies", 1f, Random.Range(1f, 5f));
    }


    void OnDisable()
    {
        instance = null;
    }


    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this; // "this" is refering to SmartPool class or the instance or an object from this class
            // think of it like it will create a constructor object
        }
    }


    public void CreateBulletAndBulletFall(GameObject bullet, GameObject bulletFall, int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject temp_Bullet = Instantiate(bullet);
            GameObject temp_Bullet_Fall = Instantiate(bulletFall);

            bullet_Prefabs.Add(temp_Bullet);
            bullet_Fall_Fx.Add(temp_Bullet_Fall);

            bullet_Prefabs[i].SetActive(false);
            bullet_Fall_Fx[i].SetActive(false);

        }
    } // Create Bullet And Bullet Fall

    public void CreateRocket(GameObject rocket, int count)
    {
        for(int i = 0; i<count; i++)
        {
            GameObject temp_Rocket_Bullet = Instantiate(rocket);

            rocket_bullet_Prefabs.Add(temp_Rocket_Bullet);

            rocket_bullet_Prefabs[i].SetActive(false);
        }
    } //create rocket


    public GameObject SpawnBulletFallFX(Vector3 position, Quaternion rotation)
    {
        for(int i = 0; i < bullet_Fall_Fx.Count; i++)
        {
            if (!bullet_Fall_Fx[i].activeInHierarchy)
            {
                bullet_Fall_Fx[i].SetActive(true);
                bullet_Fall_Fx[i].transform.position = position;
                bullet_Fall_Fx[i].transform.rotation = rotation;

                return bullet_Fall_Fx[i];
            }
        }

        return null;
    } //spawn bullet fall fx


    public void SpawnBullet(Vector3 position, Vector3 direction,Quaternion rotation, NameWeapon weaponName)
    {

        if(weaponName != NameWeapon.ROCKET)
        {
            for(int i = 0; i <bullet_Prefabs.Count; i++)
            {
                if (!bullet_Prefabs[i].activeInHierarchy)
                {
                    bullet_Prefabs[i].SetActive(true);
                    bullet_Prefabs[i].transform.position = position;
                    bullet_Prefabs[i].transform.rotation = rotation;

                    //GET THE BULLET SCRIPT
                    bullet_Prefabs[i].GetComponent<BulletController>().SetDirection(direction);

                    //SET BULLET DAMAGE
                    SetBulletDamage(weaponName, bullet_Prefabs[i]);

                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < rocket_bullet_Prefabs.Count; i++)
            {
                if (!rocket_bullet_Prefabs[i].activeInHierarchy)
                {
                    rocket_bullet_Prefabs[i].SetActive(true);
                    rocket_bullet_Prefabs[i].transform.position = position;
                    rocket_bullet_Prefabs[i].transform.rotation = rotation;

                    //GET THE BULLET SCRIPT
                    rocket_bullet_Prefabs[i].GetComponent<BulletController>().SetDirection(direction);

                    //SET BULLET DAMAGE
                    SetBulletDamage(weaponName, rocket_bullet_Prefabs[i]);
                    break;
                }
            }
        }




    } //spawn bullet 


    void SetBulletDamage(NameWeapon weaponName, GameObject bullet)
    {
        switch (weaponName)
        {
            case NameWeapon.PISTOL:
                bullet.GetComponent<BulletController>().damage = 2;
                break;

            case NameWeapon.AK:
                bullet.GetComponent<BulletController>().damage = 5;
                break;

            case NameWeapon.AWP:
                bullet.GetComponent<BulletController>().damage = 10;
                break;

            case NameWeapon.ROCKET:
                bullet.GetComponent<BulletController>().damage = 10;
                break;

            case NameWeapon.M3:
                bullet.GetComponent<BulletController>().damage = 4;
                break;

            case NameWeapon.MP5:
                bullet.GetComponent<BulletController>().damage = 3;
                break;
        }
    }

    void StartSpawningZombies()
    {
        //fence varsa, fence'in sol tarafında zombie spawnlanmayacak
        if (GameplayController.instance.gameGoal == GameGoal.DEFEND_FENCE)
        {
            float xPos = mainCamera.transform.position.x;
            xPos += 15f;

            float yPos = Random.Range(y_Spawn_Pos_Min, y_Spawn_Pos_Max);

            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0f), Quaternion.identity);
        }

        if (GameplayController.instance.gameGoal == GameGoal.KILL_ZOMBIES ||
            GameplayController.instance.gameGoal == GameGoal.TIMER_COUNTDOWN ||
            GameplayController.instance.gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            float xPos = mainCamera.transform.position.x;

            if (Random.Range(0, 2) > 0)
            {
                xPos += Random.Range(10f, 15f);
            }
            else
            {
                xPos -= Random.Range(10f, 15f);
            }

            float yPos = Random.Range(y_Spawn_Pos_Min, y_Spawn_Pos_Max);

            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0f), Quaternion.identity);


        }

        if(GameplayController.instance.gameGoal == GameGoal.GAME_OVER)
        {
            CancelInvoke("StartSpawningZombies()");
        }

    }



    }//class




























